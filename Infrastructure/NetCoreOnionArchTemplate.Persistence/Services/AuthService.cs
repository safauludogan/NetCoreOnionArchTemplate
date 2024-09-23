using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.Abstractions.Token;
using NetCoreOnionArchTemplate.Application.DTOs;
using NetCoreOnionArchTemplate.Application.DTOs.User;
using NetCoreOnionArchTemplate.Application.Exceptions;
using NetCoreOnionArchTemplate.Application.Features.Commands.Auth.Rules;
using NetCoreOnionArchTemplate.Application.Helpers;
using NetCoreOnionArchTemplate.Domain.Entities.Identity;

namespace NetCoreOnionArchTemplate.Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenHandler;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;
        private readonly AuthRules _authRules;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenHandler, IUserService userService, IMailService mailService, AuthRules authRules)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
            _userService = userService;
            _mailService = mailService;
            _authRules = authRules;
        }

        public async Task<LoginUserResponse> LoginAsync(string usernameOrEmail, string password)
        {
            AppUser? user = await _userManager.FindByNameAsync(usernameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(usernameOrEmail);

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            await _authRules.EmailAndUsernameOrPasswordShouldNotBeInvalid(user, result.Succeeded);

            IList<string> roles = await _userManager.GetRolesAsync(user);

            Token token = await _tokenHandler.CreateAccessToken(user, roles);
            await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.RefreshTokenExpiration);
            return new LoginUserResponse
            {
                Token = token,
                User = user,
            };
        }
        public Task<Token> FacebookLoginAsync(string authToken)
        {
            throw new NotImplementedException();
        }

        public Task<Token> GoogleLoginAsync(string idToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (user != null && user?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                IList<string> roles = await _userManager.GetRolesAsync(user);


                Token token = await _tokenHandler.CreateAccessToken(user, roles);
                await _userService.UpdateRefreshTokenAsync(token.RefreshToken, user, token.RefreshTokenExpiration);
                return token;
            }
            throw new AuthenticationErrorException();
        }

        public async Task PasswordResetAsync(string email)
        {
            AppUser? user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

                //Http protokolünde tırnak gibi özel karakterler sorun çıkaracağı için öncelikle üretilen token'ı byte dizisine dönüştürüyoruz.
                //Daha sonra bunu decode ederek çözümleyeceğiz.
                resetToken = resetToken.UrlEncode();

                await _mailService.SendPasswordResetMailAsync(email, user.Id, resetToken);
            }
        }

        public async Task<bool> VerifyResetTokenAsync(string resetToken, string userId)
        {
            AppUser? user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                resetToken = resetToken.UrlDecode();

                return await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", resetToken);
            }
            return false;
        }
    }
}

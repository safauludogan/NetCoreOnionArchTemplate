using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NetCoreOnionArchTemplate.Application.Abstractions.Token;
using NetCoreOnionArchTemplate.Domain.Entities.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace NetCoreOnionArchTemplate.Infrastructure.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly TokenSettings _tokenSettings;
        private readonly UserManager<AppUser> _userManager;
        public TokenService(IOptions<TokenSettings> options, UserManager<AppUser> userManager)
        {
            _tokenSettings = options.Value;
            _userManager = userManager;
        }

        public async Task<Application.DTOs.Token> CreateAccessToken(AppUser user, IList<string> roles)
        {
            Application.DTOs.Token token = new();

            //Security Key'in simetriğini alıyoruz.
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_tokenSettings.SecurityKey));

            //Şifrelenmiş kimliği oluşturuyoruz.
            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            //Oluşturulacak token ayarlarını veriyoruz.
            token.Expiration = DateTime.UtcNow.AddMinutes(_tokenSettings.TokenValidityInMinutes);
            token.RefreshTokenExpiration = DateTime.UtcNow.AddDays(_tokenSettings.RefreshTokenValidityInDays);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            JwtSecurityToken securityToken = new(
                audience: _tokenSettings.Audience,
                issuer: _tokenSettings.Issuer,
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                 claims: claims
                );

            //Token oluşturucu sınıfından bir örnek alalım.
            JwtSecurityTokenHandler tokenHandler = new();
            token.AccessToken = tokenHandler.WriteToken(securityToken);

            token.RefreshToken = CreateRefreshToken();

            await _userManager.AddClaimsAsync(user, claims);

            return token;
        }

        public string CreateRefreshToken()
        {

            byte[] number = new byte[32];
            using RandomNumberGenerator random = RandomNumberGenerator.Create();

            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}

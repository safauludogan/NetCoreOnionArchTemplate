using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.DTOs.User;
using NetCoreOnionArchTemplate.Application.Exceptions;
using NetCoreOnionArchTemplate.Application.Helpers;
using NetCoreOnionArchTemplate.Domain.Entities.Identity;
using System.Text;

namespace NetCoreOnionArchTemplate.Persistence.Services
{
	public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;

		public UserService(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<CreateUserResponse> CreateAsync(CreateUser model)
		{
			IdentityResult result = await _userManager.CreateAsync(new()
			{
				UserName = model.UserName,
				NameSurname = model.NameSurname,
				Email = model.Email,
			}, model.Password);

			CreateUserResponse response = new() { IsSuccess = result.Succeeded };
			if (result.Succeeded)
				response.Message = "User Created";
			foreach (var error in result.Errors)
			{
				response.Message += $"{error.Code} - {error.Description}\n";
			}
			return response;
		}

		public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime accessTokenDate
			, int refreshTokenLifeTime)
		{
			if (user != null)
			{
				user.RefreshToken = refreshToken;
				user.RefreshTokenEndDate = accessTokenDate.AddSeconds(refreshTokenLifeTime);
				await _userManager.UpdateAsync(user);
			}
			else
				throw new NotFoundUserException();
		}

		public async Task UpdatePasswordAsync(string userId, string resetToken, string newPassword)
		{
			AppUser? user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				resetToken = resetToken.UrlDecode();

				IdentityResult identityResult =  await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
				if (identityResult.Succeeded)
					await _userManager.UpdateSecurityStampAsync(user);
				else
					throw new PasswordChangeException();
			}
		}
	}
}

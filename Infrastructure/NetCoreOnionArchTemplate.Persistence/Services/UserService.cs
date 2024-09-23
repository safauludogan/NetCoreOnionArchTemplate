using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Application.DTOs.User;
using NetCoreOnionArchTemplate.Application.Exceptions;
using NetCoreOnionArchTemplate.Application.Helpers;
using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Domain.Entities;
using NetCoreOnionArchTemplate.Domain.Entities.Identity;
using System.Data;

namespace NetCoreOnionArchTemplate.Persistence.Services
{
    public class UserService : IUserService
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly IEndpointReadRepository _endpointReadRepository;
		public UserService(UserManager<AppUser> userManager, IEndpointReadRepository endpointReadRepository)
		{
			_userManager = userManager;
			_endpointReadRepository = endpointReadRepository;
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

		public async Task UpdateRefreshTokenAsync(string refreshToken, AppUser user, DateTime refreshTokenEndDate)
		{
			if (user != null)
			{
				user.RefreshToken = refreshToken;
				user.RefreshTokenEndDate = refreshTokenEndDate;
                await _userManager.UpdateAsync(user);
			}
			else
				throw new NotFoundUserException();
		}

		public async Task<bool> UpdatePasswordAsync(string userId, string resetToken, string newPassword)
		{
			AppUser? user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				resetToken = resetToken.UrlDecode();

				IdentityResult identityResult = await _userManager.ResetPasswordAsync(user, resetToken, newPassword);
				if (identityResult.Succeeded)
                {
                    var result = await _userManager.UpdateSecurityStampAsync(user);
                    return result.Succeeded;
                }
                else
					throw new PasswordChangeException();
			}
            return false;
        }

		public async Task<List<ListUser>> GetAllUsers(int page, int size)
		{
			List<AppUser>? userList = await _userManager.Users
				.Skip(page * size)
				.Take(size)
				.ToListAsync();

			return userList.Select(user => new ListUser
			{
				Id = user.Id,
				Email = user.Email,
				NameSurname = user.NameSurname,
				UserName = user.UserName,
				TwoFactorEnabled = user.TwoFactorEnabled
			}).ToList();
		}
		public int TotalUsersCount => _userManager.Users.Count();

		public async Task AssignRoleToUserAsync(string userId, string[] roles)
		{
			AppUser? user = await _userManager.FindByIdAsync(userId);
			if (user != null)
			{
				var userRoles = await _userManager.GetRolesAsync(user);
				await _userManager.RemoveFromRolesAsync(user, userRoles);

				await _userManager.AddToRolesAsync(user, roles);
			}
		}

		public async Task<string[]?> GetRolesToUser(string userIdOrName)
		{
			AppUser? user = new AppUser();
			string[]? currentUserRoles = new string[] { };
			try
			{
				user = await _userManager.FindByIdAsync(userIdOrName);

			}
			catch (ArgumentException ex)
			{
				user = await _userManager.FindByNameAsync(userIdOrName);
			}
			finally
			{
				if (user != null)
				{
					var userRoles = await _userManager.GetRolesAsync(user);
					currentUserRoles = userRoles.ToArray();
				}
			}
			return currentUserRoles;
		}

		public async Task<bool> HasRolePermissionToEndpointAsync(string name, string code)
		{
			var userRoles = await GetRolesToUser(name);

			if (userRoles != null && !userRoles.Any()) return false;

			Endpoint? endpoint = await _endpointReadRepository.Table
				.Include(e => e.Roles)
				.FirstOrDefaultAsync(e => e.Code == code);

			if (endpoint == null) return false;

			var endpointRoles = endpoint.Roles.Select(r => r.Name);

			foreach (var userRole in userRoles)
			{
				foreach (var endpointRole in endpointRoles)
					if (userRole == endpointRole)
						return true;

			}
			return false;
		}
    }
}

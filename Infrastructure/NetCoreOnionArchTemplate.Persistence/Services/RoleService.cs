using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetCoreOnionArchTemplate.Application.Abstractions.Services;
using NetCoreOnionArchTemplate.Domain.Entities.Identity;

namespace NetCoreOnionArchTemplate.Persistence.Services
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<AppRole> _roleManager;

		public RoleService(RoleManager<AppRole> roleManager)
		{
			_roleManager = roleManager;
		}

		public async Task<List<AppRole>> GetAllRoles(int page, int size)
		{
			if(page == -1 || size == -1)
				return await _roleManager.Roles.ToListAsync();
			return await _roleManager.Roles.Skip(page * size).Take(size).ToListAsync();
		}

		public async Task<(int id, string name)> GetRoleByIdAsync(int id)
		{
			string? role = await _roleManager.GetRoleNameAsync(new() { Id = id });
			return (id, role);
		}

		public async Task<bool> CreateRoleAsync(string name)
		{
			IdentityResult result = await _roleManager.CreateAsync(new() { Name = name });
			return result.Succeeded;
		}

		public async Task<bool> DeleteRoleAsync(int Id)
		{
			IdentityResult result = await _roleManager.DeleteAsync(new() { Id = Id });
			return result.Succeeded;
		}

		public async Task<bool> UpdateRoleAsync(int id, string name)
		{
			IdentityResult result = await _roleManager.UpdateAsync(new() { Id = id, Name = name });
			return result.Succeeded;
		}
	}
}

using NetCoreOnionArchTemplate.Domain.Entities.Identity;

namespace NetCoreOnionArchTemplate.Application.Abstractions.Services
{
	public interface IRoleService
	{
		Task<List<AppRole>> GetAllRoles(int page, int size);
		Task<(int id, string name)> GetRoleByIdAsync(int id);
		Task<bool> CreateRoleAsync(string name);
		Task<bool> DeleteRoleAsync(int Id);
		Task<bool> UpdateRoleAsync(int id,string name);
	}
}

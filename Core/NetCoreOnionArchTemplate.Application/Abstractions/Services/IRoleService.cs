using NetCoreOnionArchTemplate.Domain.Entities.Identity;

namespace NetCoreOnionArchTemplate.Application.Abstractions.Services
{
	public interface IRoleService
	{
		Task<List<AppRole>> GetAllRoles(int page, int size);
		Task<(Guid id, string name)> GetRoleByIdAsync(Guid id);
		Task<bool> CreateRoleAsync(string name);
		Task<bool> DeleteRoleAsync(Guid Id);
		Task<bool> UpdateRoleAsync(Guid id,string name);
	}
}

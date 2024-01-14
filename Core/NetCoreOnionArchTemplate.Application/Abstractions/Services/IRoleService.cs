namespace NetCoreOnionArchTemplate.Application.Abstractions.Services
{
	public interface IRoleService
	{
		IDictionary<int, string> GetAllRoles();
		Task<(int id, string name)> GetRoleByIdAsync(int id);
		Task<bool> CreateRoleAsync(string name);
		Task<bool> DeleteRoleAsync(string name);
		Task<bool> UpdateRoleAsync(int id,string name);
	}
}

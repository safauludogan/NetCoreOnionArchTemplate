namespace NetCoreOnionArchTemplate.Application.Abstractions.Services
{
	public interface IAuthorizationEndpointService
	{
		public Task AssignRoleEndpointAsync(string[] roles,string menu, string code, Type type);
	}
}

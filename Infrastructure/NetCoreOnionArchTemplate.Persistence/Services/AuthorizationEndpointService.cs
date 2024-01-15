using NetCoreOnionArchTemplate.Application.Abstractions.Services;

namespace NetCoreOnionArchTemplate.Persistence.Services
{
	public class AuthorizationEndpointService : IAuthorizationEndpointService
	{
		public Task AssignRoleEndpointAsync(string[] roles, string code)
		{
			throw new NotImplementedException();
		}
	}
}

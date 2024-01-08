using NetCoreOnionArchTemplate.Application.DTOs.Configuration;

namespace NetCoreOnionArchTemplate.Application.Abstractions.Services.Configurations
{
	public interface IApplicationService
	{
		List<Menu> GetAuthorizeDefinitionEndpoints(Type type);
	}
}

using Microsoft.Extensions.DependencyInjection;
using NetCoreOnionArchTemplate.Application.Abstractions.Hubs;
using NetCoreOnionArchTemplate.SignalR.HubServices;

namespace NetCoreOnionArchTemplate.SignalR
{
	public static class ServiceRegistration
	{
		public static void AddSignalRServices(this IServiceCollection services)
		{
			services.AddTransient<IProductHubService, ProductHubService>();
			services.AddSignalR();
		}
	}
}

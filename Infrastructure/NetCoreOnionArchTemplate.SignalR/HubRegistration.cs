
using Microsoft.AspNetCore.Builder;
using NetCoreOnionArchTemplate.SignalR.Hubs;

namespace NetCoreOnionArchTemplate.SignalR
{
	public static class HubRegistration
	{
		public static void MapHubs(this WebApplication application)
		{
			application.MapHub<ProductHub>("/product-hub");
		}
	}
}

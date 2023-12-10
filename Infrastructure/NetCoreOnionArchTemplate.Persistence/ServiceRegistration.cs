using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCoreOnionArchTemplate.Application.Abstractions;
using NetCoreOnionArchTemplate.Persistence.Concretes;
using NetCoreOnionArchTemplate.Persistence.Context;

namespace NetCoreOnionArchTemplate.Persistence
{
    public static class ServiceRegistration
    {
        //Connection String: https://www.connectionstrings.com/
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddSingleton<IProductService, ProductService>();
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.ConnectionString));
        }
    }
}

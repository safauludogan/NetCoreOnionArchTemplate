using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCoreOnionArchTemplate.Application.Repositories;
using NetCoreOnionArchTemplate.Persistence.Context;
using NetCoreOnionArchTemplate.Persistence.Repositories;

namespace NetCoreOnionArchTemplate.Persistence
{
    public static class ServiceRegistration
    {
        //Connection String: https://www.connectionstrings.com/
        public static void AddPersistanceServices(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(opt => opt.UseSqlServer(Configuration.ConnectionString));
            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using NetCoreOnionArchTemplate.Application.Abstractions.AutoMapper;

namespace NetCoreOnionArchTemplate.Mapper
{
    static public class ServiceRegistration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, AutoMapper.Mapper>();
        }
    }
}

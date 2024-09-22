using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetCoreOnionArchTemplate.Application.Base;
using NetCoreOnionArchTemplate.Application.Behaviors;
using NetCoreOnionArchTemplate.Application.Exceptions.MiddleWareException;
using System.Globalization;
using System.Reflection;

namespace NetCoreOnionArchTemplate.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddTransient<ExceptionMiddleware>();

            services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

            ///Register validators
            services.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));
        }

        private static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services,
            Assembly assembly,
            Type type)
        {
            var types = assembly.GetTypes().Where(x => x.IsSubclassOf(type) && type != x).ToList();

            foreach (var item in types)
                services.AddTransient(item);
            return services;
        }
    }
}

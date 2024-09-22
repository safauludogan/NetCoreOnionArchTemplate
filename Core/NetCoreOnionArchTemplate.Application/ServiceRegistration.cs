using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NetCoreOnionArchTemplate.Application.Behaviors;
using NetCoreOnionArchTemplate.Application.Exceptions.MiddleWareException;
using NetCoreOnionArchTemplate.Application.Features.Commands.Products.CreateProduct;
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

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

            ///Register validators
            services.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));
        }
    }
}

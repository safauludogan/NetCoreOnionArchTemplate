using Microsoft.AspNetCore.Builder;

namespace NetCoreOnionArchTemplate.Application.Exceptions.MiddleWareException
{
    public static class ConfigureExceptionMiddleware
    {
        public static void ConfigureExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}

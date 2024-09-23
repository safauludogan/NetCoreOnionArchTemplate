using NetCoreOnionArchTemplate.Application;
using NetCoreOnionArchTemplate.Persistence;
using NetCoreOnionArchTemplate.Infrastructure;
using Serilog;
using Serilog.Core;
using System.Security.Claims;
using Serilog.Context;
using NetCoreOnionArchTemplate.SignalR;
using NetCoreOnionArchTemplate.API.Filters;
using NetCoreOnionArchTemplate.API.Utility;
using NetCoreOnionArchTemplate.API;
using NetCoreOnionArchTemplate.Application.Consts;
using NetCoreOnionArchTemplate.Mapper;
using NetCoreOnionArchTemplate.Application.Exceptions.MiddleWareException;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();//Client'tan gelen request neticesinde oluşturulan HttpContext nesnesine katmanlardaki class'lar üzerinden erişebilmemizi sağlar.

builder.Services.AddPersistanceServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddSignalRServices();
builder.Services.AppApi(builder.Configuration);
builder.Services.AddCustomMapper();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
//policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
policy.WithOrigins("http://localhost.5001", "https://localhost.5001")
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
));


#region Logger
Logger log = new ProjectLogger(builder.Configuration).CreateLogger();

// Varsayılan log sağlayıcılarını temizle
builder.Logging.ClearProviders();

builder.Host.UseSerilog(log);
#endregion

builder.Services.AddControllers(options =>
{
    options.Filters.Add<RolePermissionFilter>();
});

builder.Services.AddEndpointsApiExplorer();

#region Project Environments
var env = builder.Environment;

builder.Configuration
    .SetBasePath(env.ContentRootPath)
     .AddJsonFile("appsettings.json", optional: false)
     .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())// Hata kodlarını yakalamak için.
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandlingMiddleware();// Global exception

app.UseSerilogRequestLogging();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    var email = context.User?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
    if (string.IsNullOrEmpty(email) || context.User?.Identity?.IsAuthenticated == null)
    {
        email = "Anonymous"; // Eğer email bulunamazsa varsayılan bir değer atayabilirsiniz.
    }

    LogContext.PushProperty(LoggerProperties.email, email);
    await next();
});

app.MapControllers();
app.MapHubs();

app.Run();

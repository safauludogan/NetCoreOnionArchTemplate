using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using NetCoreOnionArchTemplate.Application;
using NetCoreOnionArchTemplate.Application.Validators.Products;
using NetCoreOnionArchTemplate.Persistence;
using NetCoreOnionArchTemplate.Infrastructure;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;
using System.Security.Claims;
using Serilog.Context;
using NetCoreOnionArchTemplate.API.Extensions;
using NetCoreOnionArchTemplate.SignalR;
using NetCoreOnionArchTemplate.API.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();//Client'tan gelen request neticesinde oluşturulan HttpContext nesnesine katmanlardaki class'lar üzerinden erişebilmemizi sağlar.

builder.Services.AddPersistanceServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddSignalRServices();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
//policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
policy.WithOrigins("http://localhost.7285", "https://localhost.7285")
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
));


Logger log = new LoggerConfiguration()
	.WriteTo.Console()
	.WriteTo.File("logs/log.txt")
	.WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), "logs", autoCreateSqlTable: true,

		columnOptions: new ColumnOptions
		{
			AdditionalColumns = new Collection<SqlColumn>
			{
                //new(){ColumnName = "LogEvent",DataType = SqlDbType.NVarChar},
                new(){ColumnName = "Username",DataType = SqlDbType.NVarChar}
			}
		})
	.Enrich.FromLogContext()
	.MinimumLevel.Information()
	.CreateLogger();

builder.Host.UseSerilog(log);

builder.Services.AddControllers(options =>
{
	options.Filters.Add<RolePermissionFilter>();
})
	.AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>());

builder.Services.AddEndpointsApiExplorer();
#region Swagger
builder.Services.AddSwaggerGen(gen =>
{
	var securityScheme = new OpenApiSecurityScheme
	{
		Name = "JWT Authentication",
		Description = "Jwt Bearer Token **_only_**",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = "bearer",
		BearerFormat = "JWT",
		Reference = new OpenApiReference
		{
			Id = JwtBearerDefaults.AuthenticationScheme,
			Type = ReferenceType.SecurityScheme
		}
	};

	var vibeBilisimLink = "http://vibebilisim.com/";

	gen.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "NetCoreArch Wep Api",
		Version = "v1",
		License = new OpenApiLicense
		{
			Name = "Powered by VibeBilisim",
			Url = new Uri(vibeBilisimLink),
		},
		Contact = new OpenApiContact
		{
			Name = "Safa Uludoğan",
			Email = "safa.uludogan@vibebilisim.com.tr"
		},


	});

	gen.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
	gen.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
					{securityScheme, Array.Empty<string>()}
				});

});
#endregion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
	.AddJwtBearer("Admin", opt =>
	{
		opt.TokenValidationParameters = new()
		{
			ValidateAudience = true, // Oluşturulacak token değerinin kimlerin/hangi originlerin/sitelerin kullanıcı belirlediğimiz değerdir.
			ValidateIssuer = true, // Oluşturulacak token değerinin kimin dağıttığını ifade edeceğimiz alan
			ValidateLifetime = true, // Oluşturulan token değerinin süresini kontrol edecek olan doğrulama
			ValidateIssuerSigningKey = true, // Üretilecek token değerinin uygulamamıza ait bir değer olduğunu ifade eden suciry key verisinin doğrulamasıdır.

			ValidAudience = builder.Configuration["Token:Audience"],
			ValidIssuer = builder.Configuration["Token:Issuer"],
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
			LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,

			NameClaimType = ClaimTypes.Name //JWT üzerinde Name claimne karşılık gelen değeri User.Identity.Name propertysinden elde edebiliriz.
		};
	});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());
app.UseSerilogRequestLogging();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
	var username = context.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
	LogContext.PushProperty("Username", username);
	await next();
});

app.MapControllers();
app.MapHubs();

app.Run();

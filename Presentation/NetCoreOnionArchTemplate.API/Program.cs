using FluentValidation.AspNetCore;
using Microsoft.IdentityModel.Tokens;
using NetCoreOnionArchTemplate.Application;
using NetCoreOnionArchTemplate.Application.Validators.Products;
using NetCoreOnionArchTemplate.Persistence;
using NetCoreOnionArchTemplate.Infrastructure;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPersistanceServices();
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();

builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
//policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()
policy.WithOrigins("http://localhost.7285", "https://localhost.7285").AllowAnyHeader().AllowAnyMethod()
));

builder.Services.AddControllers()
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
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

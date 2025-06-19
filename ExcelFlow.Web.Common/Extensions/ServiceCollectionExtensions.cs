using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using ExcelFlow.Core.Interfaces;
using ExcelFlow.DataAccess.Repositories;
using ExcelFlow.Services.Interfaces;
using ExcelFlow.Services.Services;
using ExcelFlow.DataAccess.DbContexts;
using Microsoft.EntityFrameworkCore;
using ExcelFlow.Core.Configurations;
using ExcelFlow.Services.Implementations;
namespace ExcelFlow.Web.Common.Extensions;

public static class ServiceCollectionExtensions
{
    public static IConfigurationBuilder AddDefaultAppSettings(this IConfigurationBuilder builder)
    {
        var sharedPath = Path.Combine(Directory.GetParent(AppContext.BaseDirectory)!.Parent!.Parent!.Parent!.Parent!.FullName, "appsettings.shared.json");

        return builder
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile(sharedPath, optional: false, reloadOnChange: true);

    }
    public static IServiceCollection AddExcelFlowServices(this IServiceCollection services, IConfiguration configuration)
    {
        // DbContext
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        // AutoMapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        // Repositories
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IAuthRepository, AuthRepository>();

        // Services
        services.AddScoped(typeof(IBaseService<,,>), typeof(BaseService<,,>));
        services.AddScoped<IAuthService, AuthService>();

        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();
        services.Configure<RabbitMqSettings>(
    configuration.GetSection("RabbitMq"));
        services.AddSingleton<IRabbitMQPublisherService, RabbitMQPublisherService>();
        services.Configure<AwsS3Settings>(
  configuration.GetSection("AWS"));
        return services;
    }

    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var jwtSettings = configuration.GetSection("JwtSettings");
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["Issuer"],
                    ValidAudience = jwtSettings["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
                };
            });

        return services;
    }

    public static IServiceCollection AddSwaggerWithJwtSupport(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExcelFlow API", Version = "v1" });

            // JWT desteği
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT header. Örnek: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        }
                    },
                    new string[]{}
                }
            });
        });

        return services;
    }
}
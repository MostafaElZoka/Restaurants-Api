
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Application.Authentication;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Inftastructure.DishesRepos;
using Restaurant.Inftastructure.Identity.Auth;
using Restaurant.Inftastructure.Presistence;
using Restaurant.Inftastructure.RestaurantsRepos;
using Restaurant.Inftastructure.Seeders;
using System.Runtime.CompilerServices;
using System.Text;

namespace Restaurant.Inftastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("restaurantDB");
        services.AddDbContext<RestaurantDbContext>(x=>x.UseSqlServer(connectionString)
        .EnableSensitiveDataLogging());
        services.AddScoped<IRestaurantSeeder, RestaurantSeeder>();
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<IDishReposatory, DishRepository>();

        services.AddIdentity<User, IdentityRole>() // this reqisters services like usermanager, signinmanager and rolemanager
            .AddEntityFrameworkStores<RestaurantDbContext>() // this adds the identity table to the EF dbcontext
            .AddDefaultTokenProviders();     //this adds identity tokens required for features like resetting password and email confirmation(not neccessary)

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>(); 

        services.Configure<JwtSettings>(configuration.GetSection("Jwt"));//maps the jwt settings from the appsettings.json to the jwtsettings class

        var jwtsettings = configuration.GetSection("Jwt").Get<JwtSettings>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwtsettings.Issuer,
                ValidAudience = jwtsettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsettings.Key))
            };
        });


    }
}

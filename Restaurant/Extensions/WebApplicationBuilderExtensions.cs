using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Restaurant.Inftastructure.Identity.Auth;
using Restaurant.MiddleWares;
using Serilog;
using System.Text;
namespace Restaurant.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Restaurant API", Version = "v1" });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        }); //swagger authorization config

        builder.Services.AddScoped<ErrorHandlerMiddleware>();//injecting the error middleware
        builder.Services.AddScoped<TimeLoggingMiddleware>();

        builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration)); // now i am reading from the json file
        //.MinimumLevel.Override("Microsoft",LogEventLevel.Warning) i can now read and write into the json file automatically
        //.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
        //.WriteTo.File("Logs/Restaurants-API-.log",rollingInterval: RollingInterval.Day, rollOnFileSizeLimit:true)
        //.WriteTo.Console(outputTemplate: "{Timestamp:dd-MM HH:mm:ss} {Level:u3}] |{SourceContext}|{NewLine} {Message:lj}{NewLine}{Exception}"));
    }
}

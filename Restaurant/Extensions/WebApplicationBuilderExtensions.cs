using Restaurant.MiddleWares;
using Serilog;

namespace Restaurant.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

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

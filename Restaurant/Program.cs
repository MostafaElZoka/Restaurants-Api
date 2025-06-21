using Microsoft.AspNetCore.Diagnostics;
using Restaurant.Application.Extensions;
using Restaurant.Inftastructure.Extensions;
using Restaurant.Inftastructure.Seeders;
using Restaurant.MiddleWares;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;
using System.Threading.Tasks;
namespace Restaurant
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<ErrorHandlerMiddleware>();//injecting the error middleware
            builder.Services.AddScoped<TimeLoggingMiddleware>();

            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);

            builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration)); // now i am reading from the json file
            //.MinimumLevel.Override("Microsoft",LogEventLevel.Warning) i can now read and write into the json file automatically
            //.MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Information)
            //.WriteTo.File("Logs/Restaurants-API-.log",rollingInterval: RollingInterval.Day, rollOnFileSizeLimit:true)
            //.WriteTo.Console(outputTemplate: "{Timestamp:dd-MM HH:mm:ss} {Level:u3}] |{SourceContext}|{NewLine} {Message:lj}{NewLine}{Exception}"));

            var app = builder.Build();

            var scope = app.Services.CreateScope();
            var seeder=scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
            await seeder.Seed();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMiddleware<ErrorHandlerMiddleware>();//using the middleware
            app.UseMiddleware<TimeLoggingMiddleware>();

            app.UseSerilogRequestLogging(); //middleware for serilog
            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

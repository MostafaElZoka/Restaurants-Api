using Microsoft.AspNetCore.Diagnostics;
using Restaurant.Application.Extensions;
using Restaurant.Extensions;
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


            builder.AddPresentation();
            builder.Services.AddApplication();
            builder.Services.AddInfrastructure(builder.Configuration);



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

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

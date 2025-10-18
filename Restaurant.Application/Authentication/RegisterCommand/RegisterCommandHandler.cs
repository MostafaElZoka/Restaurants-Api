
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Restaurant.Application.Authentication.RegisterCommand;

public class RegisterCommandHandler(ILogger<RegisterCommandHandler> logger,UserManager<User> userManager
    ,IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Registring for user");
        try
        {
            var user = new User()
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                EmailConfirmed = false,
                PhoneNumberConfirmed = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
                TwoFactorEnabled = false
            };
            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                logger.LogError("User registration failed: {Errors}", errors);
                throw new BadHttpRequestException($"Registration failed: {string.Join(", ", errors)}",StatusCodes.Status400BadRequest);
            }

            return await jwtTokenGenerator.GenerateToken(user);
        }

        catch(DbUpdateException ex)
        {
            var inner = ex.InnerException?.Message?? ex.Message;
            throw new Exception($"Database update failed {inner}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Unhandled registration error: {ex.Message}", ex);
        }
    }
}

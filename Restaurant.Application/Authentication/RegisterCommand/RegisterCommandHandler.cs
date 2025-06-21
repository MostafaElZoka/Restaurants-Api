
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Authentication.RegisterCommand;

public class RegisterCommandHandler(ILogger<RegisterCommandHandler> logger,UserManager<User> userManager
    ,IJwtTokenGenerator jwtTokenGenerator) : IRequestHandler<RegisterCommand, string>
{
    public async Task<string> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Registring for user");
        var user = new User() { UserName = request.FullName, Email = request.Email };
        var result = await userManager.CreateAsync(user,request.Password);

        if (!result.Succeeded)
            throw new Exception("Registration failed");
        
        return await jwtTokenGenerator.GenerateToken(user);
    }
}

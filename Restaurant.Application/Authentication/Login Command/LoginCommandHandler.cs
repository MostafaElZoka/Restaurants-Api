
using MediatR;
using Microsoft.AspNetCore.Identity;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Authentication.Login_Command;

public class LoginCommandHandler(UserManager<User> userManager,SignInManager<User> signInManager
    ,IJwtTokenGenerator tokenGenerator) : IRequestHandler<LoginCommand, string>
{
    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null)
            throw new Exception("user not found");
        var result = await signInManager.CheckPasswordSignInAsync(user,request.Password,false);
        if (!result.Succeeded)
            throw new Exception("Invalid Cardentials");
        return await tokenGenerator.GenerateToken(user);
    }
}

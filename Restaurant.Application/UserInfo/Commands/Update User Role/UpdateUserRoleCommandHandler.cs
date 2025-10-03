
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Application.UserInfo.Commands.Update_User_Role;

public class UpdateUserRoleCommandHandler(ILogger<UpdateUserDetailsCommandHandler> logger,
    UserManager<User> userManager
    , RoleManager<IdentityRole> roleManager) : IRequestHandler<UpdateUserRoleCommand>
{
    public async Task Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"assigning user role: {request} ");

        var user = await userManager.FindByEmailAsync(request.Email) // we first fetch the user who we want to edit his role from the db 
            ??throw new NotFoundExceptionHandler(nameof(User), request.Email);

        var role = await roleManager.FindByNameAsync(request.RoleName) // we check whether the assigned role already exists in db or not
            ?? throw new NotFoundExceptionHandler(nameof(IdentityRole),request.RoleName);

        await userManager.AddToRoleAsync(user , role.Name!); //if exists then assign the role to the user
        
    }
}

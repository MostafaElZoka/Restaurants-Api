
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Application.UserInfo.Commands.Remove_User_From_Role;

public class RemoveUserFromRoleCommandHandler(ILogger<RemoveUserFromRoleCommandHandler> logger, UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager) : IRequestHandler<RemoveUserFromRoleCommand>
{
    public async Task Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Removing user from Role : {request}");

        var user = await userManager.FindByEmailAsync(request.Email)??throw new NotFoundExceptionHandler(nameof(User), request.Email);

        var role = await roleManager.FindByNameAsync(request.RoleName)?? throw new NotFoundExceptionHandler(nameof(IdentityRole), request.RoleName);

        await userManager.RemoveFromRoleAsync(user, role.Name!);

    }
}

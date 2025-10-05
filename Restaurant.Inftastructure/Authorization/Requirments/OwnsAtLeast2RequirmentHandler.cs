
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Application.UserInfo;
using Restaurant.Domain.Entities;
using Restaurant.Inftastructure.RestaurantsRepos;

namespace Restaurant.Inftastructure.Authorization.Requirments;

public class OwnsAtLeast2RequirmentHandler(ILogger<OwnsAtLeast2RequirmentHandler> logger, IUserContext userContext, UserManager<User> userManager) : AuthorizationHandler<OwnsAtLeast2Requirment>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OwnsAtLeast2Requirment requirement)
    {
        var user = userContext.GetCurrentUser();

        //var dbUser = await userManager.Users.Include(u => u.RestaurantsOwned).FirstOrDefaultAsync(u => u.Id == user.Id);

        //var count = dbUser.RestaurantsOwned.Count();
         var count = await userManager.Users
            .Where(u => u.Id == user.Id)
            .SelectMany(u => u.RestaurantsOwned)
            .CountAsync();

        if (count < 2)
        {
            context.Fail();
            return;
        }
        else
        {
            context.Succeed(requirement);
        }
    }
}

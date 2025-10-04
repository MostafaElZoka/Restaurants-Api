
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurant.Application.UserInfo;

namespace Restaurant.Inftastructure.Authorization.Requirments;

public class MinimumAgeRequirmentHandler(IUserContext userContext, ILogger<MinimumAgeRequirmentHandler> logger) : AuthorizationHandler<MinimumAgeRequirment>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirment requirement)
    {
        var user = userContext.GetCurrentUser();

        if(user.DateOfBirth == null)
        {
            logger.LogWarning("date of birth is null");
            context.Fail();
            return Task.CompletedTask;
        }

        if (user.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorization succeeded");
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }
        return Task.CompletedTask;

    }
}

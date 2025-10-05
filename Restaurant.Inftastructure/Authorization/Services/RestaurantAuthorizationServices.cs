
using Microsoft.Extensions.Logging;
using Restaurant.Application.UserInfo;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Inftastructure.Authorization.Services
{
    public class RestaurantAuthorizationServices(ILogger<RestaurantAuthorizationServices> logger, IUserContext userContext) : IRestaurantAuthorizationServices
    {
        public bool Authorize(Restaurantt restaurant, ResourceOperations resourceOperations)
        {
            var user = userContext.GetCurrentUser();

            if (resourceOperations == ResourceOperations.Create || resourceOperations == ResourceOperations.Read)
            {
                logger.LogInformation("create/read - successful authorization ");
                return true;
            }

            if (resourceOperations == ResourceOperations.Delete && user.IsInRole(UserRoles.Admin))
            {
                logger.LogInformation("admin is deleting restaurnat - successful authorization");
                return true;
            }

            if ((resourceOperations == ResourceOperations.Delete || resourceOperations == ResourceOperations.Update)
                && user.Id == restaurant.OwnerId)
            {
                logger.LogInformation("owner delete/update - successful authorization");
                return true;
            }

            return false;
        }
    }
}

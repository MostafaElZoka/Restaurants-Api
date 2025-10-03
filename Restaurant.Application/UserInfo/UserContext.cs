
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Restaurant.Application.UserInfo;

public interface IUserContext
{
    CurrentUser? GetCurrentUser();
}
public class UserContext(IHttpContextAccessor httpContextAccessor) : IUserContext // a service to get the user info from the token
{
    public CurrentUser? GetCurrentUser()
    {
        var user = httpContextAccessor?.HttpContext?.User;
        if (user == null)
        {
            throw new InvalidOperationException("User context is not present");
        }

        if (user.Identity == null || !user.Identity.IsAuthenticated )
        {
            return null;
        }

        var email = user.FindFirst(c => c.Type == ClaimTypes.Email)!.Value;
        var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        var roles = user.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).ToArray();

        return new CurrentUser(userId, email, roles); //saving the info inside a record
    }
}

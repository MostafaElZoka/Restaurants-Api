
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
        var nationality = user.FindFirst(c => c.Type == "Nationality")?.Value;
        var dobString = user.FindFirst(c=>c.Type == "DateOfBirth")?.Value;

        var dob = dobString == null ? (DateOnly?)null : DateOnly.ParseExact(dobString,"yyyy-MM-dd");
        return new CurrentUser(userId, email, roles, nationality, dob); //saving the info inside a record
    }
}

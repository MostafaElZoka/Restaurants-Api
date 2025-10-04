
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Restaurant.Domain.Entities;
using System.Security.Claims;

namespace Restaurant.Inftastructure.Authorization;

public class RestaurantsUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, IdentityRole>
{
    UserManager<User> _userManager;
    RoleManager<IdentityRole> _roleManager;
    IOptions<IdentityOptions> _options;
    public RestaurantsUserClaimsPrincipalFactory(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
    {
        _userManager = userManager;
        _options = options;
        _roleManager = roleManager;
    }
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        var id = await GenerateClaimsAsync(user); //id is the object of the claims of the user

        if(user.Nationality != null)
        {
            id.AddClaim(new Claim("Nationality", user.Nationality));
        }
        if(user.DateOfBirth != null)
        {
            id.AddClaim(new Claim("DateOfBirth", user.DateOfBirth.Value.ToString("yyyy-MM-dd")));
        }

        return new ClaimsPrincipal(id);
    }
}

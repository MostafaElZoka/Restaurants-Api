using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Application.Authentication;
using Restaurant.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Inftastructure.Identity.Auth;

public class JwtTokenGenerator(IOptions<JwtSettings> jwtsettings,UserManager<User> userManager) : IJwtTokenGenerator
{
    private readonly JwtSettings _JwtSettings = jwtsettings.Value; //this gets a mapped jwtsettings obj 
    public async Task<string> GenerateToken(User user)
    {
        if(user.Email ==  null)
        {
            throw new Exception("Email can't be empty");
        }
        var roles = await userManager.GetRolesAsync(user);
        var Claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.Id),//sub is used for saving the id
            new Claim("fullName", user.FullName),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim("DateOfBirth", user.DateOfBirth!.Value.ToString("yyyy-MM-dd")),
            new Claim("Nationality", user.Nationality!)
        };
        Claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));  

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JwtSettings.SecretKey));
        var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _JwtSettings.Issuer,
            audience: _JwtSettings.Audience,
            claims: Claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials:creds
            );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

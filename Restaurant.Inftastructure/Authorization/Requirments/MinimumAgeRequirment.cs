
using Microsoft.AspNetCore.Authorization;

namespace Restaurant.Inftastructure.Authorization.Requirments;

public class MinimumAgeRequirment : IAuthorizationRequirement
{
    public int MinimumAge { get; set; }
}

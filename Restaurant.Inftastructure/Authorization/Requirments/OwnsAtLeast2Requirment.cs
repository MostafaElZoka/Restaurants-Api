
using Microsoft.AspNetCore.Authorization;

namespace Restaurant.Inftastructure.Authorization.Requirments;

public class OwnsAtLeast2Requirment : IAuthorizationRequirement
{
    public int OwnedRestaurantsCount { get; set; }
}

using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastucture.Authorization.Requirements;

public class MinimumAgeRequirement(int minimumAge) : IAuthorizationRequirement
{
    public int MinimumAge { get; } = minimumAge;

   
}

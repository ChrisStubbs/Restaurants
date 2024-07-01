using Microsoft.AspNetCore.Authorization;

namespace Restaurants.Infrastucture.Authorization.Requirements;

internal class CreatedMultipleRestaurantsRequirement(int minimumRestaurantsCreated) : IAuthorizationRequirement
{
    public int MinimumRestaurantsCreated { get; } = minimumRestaurantsCreated;


}

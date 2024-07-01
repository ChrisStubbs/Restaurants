using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;
using Restaurants.Domain.Repositories;

namespace Restaurants.Infrastucture.Authorization.Requirements;

internal class CreatedMultipleRestaurantsRequirementHandler(
    ILogger<CreatedMultipleRestaurantsRequirementHandler> logger,
    IRestaurantsRepository restaurantsRepository,
    IUserContext userContext): AuthorizationHandler<CreatedMultipleRestaurantsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CreatedMultipleRestaurantsRequirement requirement)
    {
        var currentUser = userContext.GetCurrentUser();

        if (currentUser is null)
        {
            logger.LogInformation("Authorization failed user is not authenticated");
            context.Fail();
            return;
        }

        logger.LogInformation("User : {Email} - Handling internal class CreatedMultipleRestaurantsRequirementHandler(\r\n", currentUser.Email);

        var restaurants = await restaurantsRepository.GetAllAsync();

        var userRestaurantsCreated = restaurants.Count(r => r.OwnerId == currentUser.Id);

        if(userRestaurantsCreated >= requirement.MinimumRestaurantsCreated)
        {
            logger.LogInformation("Authorization succeded");
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("Authorization failed user has not created enough restaurants");
            context.Fail();
        }


        
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Users;

namespace Restaurants.Infrastucture.Authorization.Requirements;

internal class MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirement> logger,
    IUserContext userContext) : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
    {
       var currentUser = userContext.GetCurrentUser();
        if (currentUser is null)
        {
            logger.LogInformation("Authorization failed user is not authenticated");
            context.Fail();
            return Task.CompletedTask;
        }

        logger.LogInformation("User : {Email}, date of birth {Dob} - Handling minimumAgeRequirement",
            currentUser.Email,
            currentUser.DateOfBirth);

        if(currentUser.DateOfBirth is null)
        {
            logger.LogInformation("Authorization failed user date of birth is null");
            context.Fail();
            return Task.CompletedTask;
        }

        if(currentUser.DateOfBirth.Value.AddYears(requirement.MinimumAge) <= DateOnly.FromDateTime(DateTime.Today))
        {
            logger.LogInformation("Authorization succeded");
            context.Succeed(requirement);
        }
        else
        {
            logger.LogInformation("Authorization failed user is not old enough");
            context.Fail();
        }

        return Task.CompletedTask;
    }
}

using FluentValidation.TestHelper;
using Restaurants.Application.Restaurants.Commands.CreateRestaurant;
using Xunit;

namespace Restaurants.Application.Tests.Restaurants.Commands.CreateRestaurant
{
    public class CreateRestaurantCommandValidatorTests
    {
        //TestMethod_Scenario_ExpectedResult
        [Fact()]
        public void Validator_ForValidCommand_ShouldNotHaveValidationErrors()
        {
            // Arrange
            var command = new CreateRestaurantCommand
            {
                Name = "Test Restaurant",
                Category = "Italian",
                ContactEmail = "test@test.com",
                PostalCode = "12345-123"
            };

            var validator = new CreateRestaurantCommandValidator();

            // Act

            var result = validator.TestValidate(command);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
using FluentValidation;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant;

public class CreateRestaurantCommandValidator : AbstractValidator<CreateRestaurantCommand>
{
    private readonly string[] _validCategories = { "Italian", "Mexican", "Japanese", "American", "Indian", "Vegetarian", "French" };
    public CreateRestaurantCommandValidator()
    {
        RuleFor(x => x.Name).Length(3, 100);
        RuleFor(x => x.Category).Must(_validCategories.Contains).WithMessage($"Invalid category. Please choose from the valid categories: {string.Join(", ", _validCategories)}");
        RuleFor(x => x.ContactEmail).EmailAddress().WithMessage("Please provide a valid email address");
        RuleFor(x => x.PostalCode).Matches(@"^\d{5}-\d{3}$").WithMessage("Please provide a valid postcode (XXXXX-XXX)"); ;
    }
}

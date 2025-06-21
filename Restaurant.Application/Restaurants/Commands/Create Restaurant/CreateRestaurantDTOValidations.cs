using FluentValidation;
using Restaurant.Application.Restaurants.Commands.Create_Restaurant;
using Restaurant.Application.Restaurants.DTOs;


namespace Restaurant.Application.Validators;

public class CreateRestaurantDTOValidations:AbstractValidator<CreateRestaurantCommand> // we changed the type to be createrestaurantcommand instead of dto
{
    private readonly List<string> validCategories = new() {"Fast Food", "Japanese", "Italian", "American" };
    public CreateRestaurantDTOValidations()
    {
        RuleFor(dto => dto.Name)
            .Length(3, 100);

        RuleFor(dto => dto.Description)
            .NotEmpty().WithMessage("enter a valid descriotion");

        RuleFor(dto => dto.Category)
            .Must(c=>validCategories.Contains(c)).WithMessage("enter a valid category");

        //RuleFor(dto => dto.Category)
        //.Custom((value, contxt) =>
        //{
        //    var isValid = validCategories.Contains(value);
        //    if (!isValid)
        //    {
        //        contxt.AddFailure("Category", "Invalid Category");
        //    }
        //});

        RuleFor(dto => dto.ContactEmail)
            .EmailAddress().WithMessage("enter a valid email format");

        RuleFor(dto => dto.PostalCode)
            .Matches(@"^\d{2}-\d{3}$").WithMessage("Please provide a valid postal code (XX-XXX)");
    }
}

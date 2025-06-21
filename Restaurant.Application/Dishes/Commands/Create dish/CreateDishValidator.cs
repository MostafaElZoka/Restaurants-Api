
using FluentValidation;

namespace Restaurant.Application.Dishes.Commands.Create_dish;

public class CreateDishValidator:AbstractValidator<CreateDishCommand>
{
    public CreateDishValidator()
    {
        RuleFor(dish => dish.KiloCalories).GreaterThanOrEqualTo(0).
            WithMessage("kilocalories must be greater than or equal zero");
        RuleFor(dish => dish.Price).GreaterThan(0).
            WithMessage("price must be greater than or equal zero");
    }
}


using FluentValidation;
using Restaurant.Application.Restaurants.DTOs;

namespace Restaurant.Application.Restaurants.Queries.Get_All_Restaurants;

public class GetAllRestaurantsQueryValidator : AbstractValidator<GetAllRestaurantsQuery>
{
    int[] allowedSizes = [5, 10, 15, 20 , 25, 30];
    string[] allowedSortingKeyWords = [nameof(RestaurantsDTO.Name), nameof(RestaurantsDTO.Description), nameof(RestaurantsDTO.Category)];
    public GetAllRestaurantsQueryValidator()
    {
        RuleFor(r => r.pageSize)
            .Must(value => allowedSizes.Contains(value))
            .WithMessage("Page size must be" + string.Join(", ", allowedSizes));

        RuleFor(r => r.pageNumber)
            .GreaterThan(0)
            .WithMessage("Page Number must be greater than 1");

        RuleFor(r => r.SortBy)
            .Must(value => allowedSortingKeyWords.Contains(value))
            .When(r => r.SortBy != null)    
            .WithMessage($"Sorting keyword must be in [{string.Join(", ", allowedSortingKeyWords)}]");
    }
}

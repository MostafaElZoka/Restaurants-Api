
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.Dishes.Commands;

public class CreateDishCommand:IRequest<int>
{
    [Required]
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }
    public int RestaurantId { get; set; }
}

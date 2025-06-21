
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Application.Restaurants.Commands.Update_Restaurant;

public class UpdateRestaurantCommand:IRequest
{
    public int Id { get; set; }
    [StringLength(100,MinimumLength =3, ErrorMessage ="length must be between 3-100 charachters")]
    public string Name { get; set; } = default!;
    public string? Description { get; set; } = default!;
    public bool HasDelivery { get; set; }
}

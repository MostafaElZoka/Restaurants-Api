
using MediatR;

namespace Restaurant.Application.Restaurants.Commands.Delete_Restaurant;

public class DeleteRestaurantCommand:IRequest
{
    public int Id { get; set; }
}

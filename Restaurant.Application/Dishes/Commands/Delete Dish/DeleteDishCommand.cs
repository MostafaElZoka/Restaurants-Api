
using MediatR;

namespace Restaurant.Application.Dishes.Commands.Delete_Dish;

public class DeleteDishCommand:IRequest
{
    public int restaurantId { get; set; }
    public int dishId { get; set; }
}

using MediatR;

namespace Restaurant.Application.Dishes.Commands.Update_Dish;

public class UpdateDishCommand:IRequest
{
    public int DishId { get; set; }
    public int RestaurnatId { get; set; }
    public int Price { get; set; }
}

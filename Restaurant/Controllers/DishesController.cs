using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Dishes.Commands;
using Restaurant.Application.Dishes.Commands.Delete_Dish;
using Restaurant.Application.Dishes.Commands.Update_Dish;
using Restaurant.Application.Dishes.DTOs;
using Restaurant.Application.Dishes.Queries;
using Restaurant.Application.Dishes.Queries.Get_Dish_By_Id;
using Restaurant.Domain.Entities;
using Restaurant.Inftastructure.Authorization;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [ApiController]
    [Route("api/restaurants/{restaurantId}/dishes")]
    [Authorize]
    public class DishesController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateDish([FromRoute] int restaurantId, CreateDishCommand command)
        {
            command.RestaurantId = restaurantId; // we must bind the id from the route to the id in the body
            var dishId = await mediator.Send(command);
            return CreatedAtAction(nameof(GetDishById),new {restaurantId, dishId },null);
        }
        [HttpGet]
        [Authorize(Policy =PolicyNames.LessThan20)]
        public async Task<ActionResult<IEnumerable<DishesDTO>>> GetAllDishesForRestaurant([FromRoute] int restaurantId)
        {
            var dishes = await mediator.Send(new GetAllDishesQuery { RestaurantId = restaurantId });
            return Ok(dishes);
        }
        [HttpGet("{DishId}")]
        public async Task<ActionResult<DishesDTO>> GetDishById([FromRoute] int restaurantId, [FromRoute]int DishId)
        {
            var dish = await mediator.Send(new GetDishByIdQuery() { DishId=DishId, RestaurantId=restaurantId});
            return Ok(dish);
        }
        [HttpDelete("{DishId}")]
        public async Task<IActionResult> DeleteDish([FromRoute]int DishId, [FromRoute]int restaurantId)
        {
            var command = new DeleteDishCommand()
            {
                dishId = DishId,
                restaurantId = restaurantId
            };
            await mediator.Send(command);
            return NoContent();
        }
        [HttpPatch("{DishId}")]
        public async Task<IActionResult> UpdateDish([FromRoute] int DishId, [FromRoute] int restaurantId, UpdateDishCommand command)
        {
            command.RestaurnatId = restaurantId;
            command.DishId = DishId;
            await mediator.Send(command);
            return NoContent();
        }
    }
}

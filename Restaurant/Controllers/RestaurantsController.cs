using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Restaurants;
using Restaurant.Application.Restaurants.Commands.Create_Restaurant;
using Restaurant.Application.Restaurants.Commands.Delete_Restaurant;
using Restaurant.Application.Restaurants.Commands.Update_Restaurant;
using Restaurant.Application.Restaurants.DTOs;
using Restaurant.Application.Restaurants.Queries;
using Restaurant.Application.Restaurants.Queries.Get_Restaurant_By_Id;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//as long as it has no other options then the annotation here is for authentication 
    public class RestaurantsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<RestaurantsDTO>))] // this is to set the expected returned status code and the type of the returned object
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<RestaurantsDTO>>> GetAllRestaurants()
        {
            var restaurants =await mediator.Send(new GetAllRestaurantsQuery());
            return Ok(restaurants); 
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<RestaurantsDTO>> GetRestaurantById([FromRoute]int id)
        {
            var restaurant = await mediator.Send(new GetRestaurantByIdQuery() { Id = id});
            //if(restaurant == null)
            //{
            //    return NotFound("restaurant with that id doesn't exist");
            //}
            return Ok(restaurant);
        }

        [HttpPost]
        [Authorize(Roles =UserRoles.Owner)]//only owners can create restaurants 
        public async Task<IActionResult> CreateRestaurant([FromBody] CreateRestaurantCommand command)
        {
            int id = await mediator.Send(command);
            return  CreatedAtAction(nameof(GetRestaurantById), new {id}, null);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRestaurant([FromRoute]int id)
        {
            //var IsDeleted = await mediator.Send(new DeleteRestaurantCommand() { Id = id });
            await mediator.Send(new DeleteRestaurantCommand() { Id = id});

            //return NotFound(); there's no need to return not found because the notfoundexception we made will be fired by mediator
            return NoContent();
        }
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateRestaurant([FromBody]UpdateRestaurantCommand command, [FromRoute]int id)
        {
            command.Id = id;
            //var IsUpdated = await mediator.Send(command);
            await mediator.Send(command);
            
                return NoContent();

            //return NotFound(); there's no need to return not found because the notfoundexception we made will be fired by mediator
        }
    }
}

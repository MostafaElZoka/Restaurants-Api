
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Commands.Update_Dish;

public class UpdateDishCommandHandler(IRestaurantRepository restaurantRepository
    ,ILogger<UpdateDishCommandHandler> logger,IDishReposatory dishReposatory,IMapper mapper,
    IRestaurantAuthorizationServices restaurantAuthorizationServices) : IRequestHandler<UpdateDishCommand>
{
    public async Task Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"updating dish with restaurant id:{request.RestaurnatId} and dish id:{request.DishId}");

        var restaurant = await restaurantRepository.GetById(request.RestaurnatId);

        if (restaurant == null)
            throw new NotFoundExceptionHandler(nameof(restaurant), request.RestaurnatId.ToString());

        if (!restaurantAuthorizationServices.Authorize(restaurant, ResourceOperations.Update)) //if the user is not the owner of the restaurant
            throw new ForbidException();

        var dish = await dishReposatory.GetById(request.DishId,restaurant.Id);
         mapper.Map(request, dish);
        await dishReposatory.Update(dish);
    }
}


using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Commands.Update_Dish;

public class UpdateDishCommandHandler(IRestaurantRepository restaurantRepository
    ,ILogger<UpdateDishCommandHandler> logger,IDishReposatory dishReposatory,IMapper mapper) : IRequestHandler<UpdateDishCommand>
{
    public async Task Handle(UpdateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"updating dish with restaurant id:{request.RestaurnatId} and dish id:{request.DishId}");
        var restaurant = await restaurantRepository.GetById(request.RestaurnatId);
        if (restaurant == null)
            throw new NotFoundExceptionHandler(nameof(restaurant), request.RestaurnatId.ToString());
        var dish = await dishReposatory.GetById(request.DishId,restaurant.Id);
         mapper.Map(request, dish);
        await dishReposatory.Update(dish);
    }
}

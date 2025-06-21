
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Commands.Delete_Dish;

public class DeleteDishCommandHandler(IRestaurantRepository restaurantRepository, ILogger<DeleteDishCommandHandler> logger
    ,IDishReposatory dishReposatory) : IRequestHandler<DeleteDishCommand>
{
    public async Task Handle(DeleteDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogWarning($"Deleting dish with restaurant id:{request.restaurantId} and dish id:{request.dishId}");
        var restaurant = await restaurantRepository.GetById(request.restaurantId);
        if (restaurant == null ) 
            throw new NotFoundExceptionHandler(nameof(restaurant),request.restaurantId.ToString());
        var dish = await dishReposatory.GetById(request.dishId,restaurant.Id);
        //var result = mapper.Map<Dish>(dish);
        await dishReposatory.Delete(dish);
    }
}

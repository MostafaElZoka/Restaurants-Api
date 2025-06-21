
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Commands;

public class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
    IRestaurantRepository restaurantRepository, IDishReposatory dishReposatory, IMapper mapper) : IRequestHandler<CreateDishCommand,int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new dish");
        var restaurant = await restaurantRepository.GetById(request.RestaurantId);
        if (restaurant == null)
        {
            throw new NotFoundExceptionHandler(nameof(restaurant), request.RestaurantId.ToString());
        }
        var dish = mapper.Map<Dish>(request);
        return await dishReposatory.Create(dish);
    }
}

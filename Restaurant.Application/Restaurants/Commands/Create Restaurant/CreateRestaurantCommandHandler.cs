

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.Create_Restaurant;

public class CreateRestaurantCommandHandler(IMapper mapper, IRestaurantRepository restaurantRepository,ILogger<CreateRestaurantCommandHandler> logger) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creating a new Restaurant {@restaurant}",request);
        var restaurant = mapper.Map<Restaurantt>(request); // we map first because the infrastructure knows nothing about dtos
        int id = await restaurantRepository.Create(restaurant);
        return id;
    }
}

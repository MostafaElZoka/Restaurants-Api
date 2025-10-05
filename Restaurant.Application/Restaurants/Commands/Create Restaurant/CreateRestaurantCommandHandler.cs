

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.UserInfo;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.Create_Restaurant;

public class CreateRestaurantCommandHandler(IMapper mapper, IRestaurantRepository restaurantRepository,ILogger<CreateRestaurantCommandHandler> logger, IUserContext userContext) : IRequestHandler<CreateRestaurantCommand, int>
{
    public async Task<int> Handle(CreateRestaurantCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        if(currentUser == null)
        {
            throw new NotFoundExceptionHandler(nameof(User), "123");
        }
        logger.LogInformation($"User with Email: {currentUser.Email} is Creating a new Restaurant: {request}");
        var restaurant = mapper.Map<Restaurantt>(request); // we map first because the infrastructure knows nothing about dtos

        restaurant.OwnerId = currentUser!.Id; // we add the owner of the restaurant to the table
        int id = await restaurantRepository.Create(restaurant);
        return id;
    }
}

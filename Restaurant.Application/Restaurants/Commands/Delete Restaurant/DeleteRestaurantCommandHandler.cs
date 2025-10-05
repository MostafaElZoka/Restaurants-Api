
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.Delete_Restaurant;

public class DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository,
    ILogger<DeleteRestaurantCommandHandler> logger, 
    IRestaurantAuthorizationServices restaurantAuthorizationServices) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Restaurant with id {id}",request.Id);

        var restaurant = await restaurantRepository.GetById(request.Id);

        if (restaurant == null)
            //return false;
           throw new NotFoundExceptionHandler(nameof(restaurant),request.Id.ToString());

        if (!restaurantAuthorizationServices.Authorize(restaurant, ResourceOperations.Delete)) //if the user is not an admin or owner
            throw new ForbidException();

        await restaurantRepository.Delete(restaurant);

        //return true;
    }
}

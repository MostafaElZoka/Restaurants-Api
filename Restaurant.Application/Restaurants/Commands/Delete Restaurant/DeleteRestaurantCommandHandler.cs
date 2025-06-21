
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.Delete_Restaurant;

public class DeleteRestaurantCommandHandler(IRestaurantRepository restaurantRepository, ILogger<DeleteRestaurantCommandHandler> logger) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Deleting Restaurant with id {id}",request.Id);
        var restaurant = await restaurantRepository.GetById(request.Id);
        if (restaurant == null)
            //return false;
           throw new NotFoundExceptionHandler(nameof(restaurant),request.Id.ToString());
        await restaurantRepository.Delete(restaurant);

        //return true;
    }
}

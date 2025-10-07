using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.Update_Restaurant;

public class UpdateRestaurantCommandHandler(IRestaurantRepository restaurantRepository,
    IMapper mapper,
    ILogger<UpdateRestaurantCommandHandler> logger,
    IRestaurantAuthorizationServices restaurantAuthorizationServices) : IRequestHandler<UpdateRestaurantCommand>
{
    public async Task Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Updating Restaurant with id {restaurantId} with {@updatedRestaurant}",request.Id,request);

        var restaurant = await restaurantRepository.GetById(request.Id); 

        if (restaurant == null)
        {
            //return false;
           throw new NotFoundExceptionHandler(nameof(Restaurantt),request.Id.ToString());
        }

        if (!restaurantAuthorizationServices.Authorize(restaurant, ResourceOperations.Update)) //if the user is not the owner of the restaurant
            throw new ForbidException();

        var mapped = mapper.Map(request,restaurant); // this applies the mapping to the current object
        //restaurant.Name = request.Name;
        //restaurant.Description = request.Description;
        //restaurant.HasDelivery = request.HasDelivery;
        await restaurantRepository.Update(mapped);
        //return true;
    }
}

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.DTOs;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Queries.Get_Dish_By_Id;

public class GetDishByIdQueryHandler(IRestaurantRepository restaurantRepository,ILogger<GetDishByIdQueryHandler> logger,IMapper mapper)
    : IRequestHandler<GetDishByIdQuery, DishesDTO>
{
    public async Task<DishesDTO> Handle(GetDishByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("getting Dish by its ID");
        var restaurant = await restaurantRepository.GetById(request.RestaurantId);
        if (restaurant == null)
            throw new NotFoundExceptionHandler(nameof(restaurant),request.RestaurantId.ToString());

        //var dish = await dishReposatory.GetById(request.DishId, request.RestaurantId);
        var dish = mapper.Map<DishesDTO>(restaurant.Dishes); //we can map directly instead if using the dishrepo methods
        return dish;
    }
}

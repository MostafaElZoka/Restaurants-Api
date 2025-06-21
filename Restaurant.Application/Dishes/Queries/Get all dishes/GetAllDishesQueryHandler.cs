using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Dishes.DTOs;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Dishes.Queries.Get_all_dishes;

public class GetAllDishesQueryHandler(IRestaurantRepository restaurantRepository
    ,ILogger<GetAllDishesQueryHandler> logger, IMapper mapper)
    : IRequestHandler<GetAllDishesQuery, IEnumerable<DishesDTO>>
{
    public async Task<IEnumerable<DishesDTO>> Handle(GetAllDishesQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("getting all dishes");
        var restaurant = await restaurantRepository.GetById(request.RestaurantId);
        if (restaurant == null)
            throw new NotFoundExceptionHandler(nameof(restaurant),request.RestaurantId.ToString());
       //var dishes= await dishReposatory.GetAll(restaurant.Id);
        var dishesDto = mapper.Map<IEnumerable<DishesDTO>>(restaurant.Dishes);  
        return dishesDto;
    }
}

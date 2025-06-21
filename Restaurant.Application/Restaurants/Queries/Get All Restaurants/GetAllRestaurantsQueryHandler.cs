
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurants.DTOs;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Queries.Get_All_Restaurants;

public class GetAllRestaurantsQueryHandler(IRestaurantRepository restaurantRepository, IMapper mapper,
    ILogger<GetAllRestaurantsQueryHandler> Logger) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantsDTO>>
{
    public async Task<IEnumerable<RestaurantsDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantRepository.GetAllAsync();
        var DTOs = mapper.Map<IEnumerable<RestaurantsDTO>>(restaurants);
        return DTOs!;
    }
}


using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Common;
using Restaurant.Application.Restaurants.DTOs;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Queries.Get_All_Restaurants;

public class GetAllRestaurantsQueryHandler(IRestaurantRepository restaurantRepository, IMapper mapper,
    ILogger<GetAllRestaurantsQueryHandler> Logger) : IRequestHandler<GetAllRestaurantsQuery, PagedResult<RestaurantsDTO>>
{
    public async Task<PagedResult<RestaurantsDTO>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
    {
        Logger.LogInformation("Getting all restaurants");

        var (restaurants, totalCount) = (await restaurantRepository.GetAllMatching(request.searchPhrase, 
                                                                                    request.pageSize, 
                                                                                    request.pageNumber,
                                                                                    request.SortBy,
                                                                                    request.SortDirection));
            
        var DTOs = mapper.Map<IEnumerable<RestaurantsDTO>>(restaurants);
        //return DTOs!;

        return new PagedResult<RestaurantsDTO>(DTOs, totalCount, request.pageSize, request.pageNumber);
    }
}

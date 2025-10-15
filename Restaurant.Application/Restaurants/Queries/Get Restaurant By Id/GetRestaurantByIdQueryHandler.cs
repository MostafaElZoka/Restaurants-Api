
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurant.Application.Restaurants.DTOs;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Queries.Get_Restaurant_By_Id;

public class GetRestaurantByIdQueryHandler(IMapper mapper,
    IRestaurantRepository restaurantRepository, ILogger<GetRestaurantByIdQueryHandler> logger,
    IBlobStrorageService blobStrorageService) : IRequestHandler<GetRestaurantByIdQuery, RestaurantsDTO>
{
    public async Task<RestaurantsDTO> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Get restaurant with Id {id}",request.Id);
        var restaurant = await restaurantRepository.GetById(request.Id);
        if(restaurant == null)
        {
            throw new NotFoundExceptionHandler(nameof(request), request.Id.ToString());
        }
        //var DTO = RestaurantsDTO.FromEntity(restaurant);
        var DTO = mapper.Map<RestaurantsDTO>(restaurant);
        DTO.LogoSasUrl = blobStrorageService.GetBlobSas(restaurant.LogoUrl);
        return DTO;
    }
}


using MediatR;
using Restaurant.Application.Restaurants.DTOs;

namespace Restaurant.Application.Restaurants.Queries.Get_Restaurant_By_Id;

public class GetRestaurantByIdQuery:IRequest<RestaurantsDTO> //may return null so nullabe dto
{
    public int Id { get; set; }
}


using MediatR;
using Restaurant.Application.Restaurants.DTOs;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurants.Queries;

public class GetAllRestaurantsQuery:IRequest<IEnumerable<RestaurantsDTO>> //we return a dto 

{

}


using MediatR;
using Restaurant.Application.Common;
using Restaurant.Application.Restaurants.DTOs;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurants.Queries;

public class GetAllRestaurantsQuery:IRequest<PagedResult<RestaurantsDTO>> //we return a dto 

{
    public string? searchPhrase { get; set; }
    public int pageSize { get; set; }
    public int pageNumber { get; set; }
    public string? SortBy { get; set; }
    public SortDirection? SortDirection { get; set; }
}

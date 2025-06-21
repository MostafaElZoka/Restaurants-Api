
using MediatR;
using Restaurant.Application.Dishes.DTOs;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Dishes.Queries;

public class GetDishByIdQuery:IRequest<DishesDTO>
{
    public int DishId { get; set; }
    public int RestaurantId { get; set; }
}

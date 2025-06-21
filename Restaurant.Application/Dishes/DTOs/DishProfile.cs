
using AutoMapper;
using Restaurant.Application.Dishes.Commands;
using Restaurant.Application.Dishes.Commands.Update_Dish;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Dishes.DTOs;

public class DishProfile:Profile
{
    public DishProfile()
    {
        CreateMap<Dish, DishesDTO>().ReverseMap();
        CreateMap<CreateDishCommand, Dish>();
        CreateMap<UpdateDishCommand, Dish>();
    }
}

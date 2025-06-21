using AutoMapper;
using Restaurant.Application.Restaurants.Commands.Create_Restaurant;
using Restaurant.Application.Restaurants.Commands.Update_Restaurant;
using Restaurant.Domain.Entities;


namespace Restaurant.Application.Restaurants.DTOs;

public class RestaurantProfile:Profile
{
    public RestaurantProfile()
    {
        CreateMap<UpdateRestaurantCommand, Restaurantt>();

        CreateMap<CreateRestaurantCommand, Restaurantt>() //modified the source to be createRestaurantCommand instead of DTO
            .ForMember(d=>d.Address, opt=>opt.MapFrom(dto=> new Address
            {
                City = dto.City,
                Street = dto.Street,
                PostalCode = dto.PostalCode,
            }));

        CreateMap<Restaurantt, RestaurantsDTO>().
            ForMember(d => d.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))/// ? operator dont work with lambda expressions so we use the old ternary operator
            .ForMember(d => d.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))/// ? operator dont work with lambda expressions so we use the old ternary operator
            .ForMember(d => d.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
            .ForMember(d => d.Dishes, opt => opt.MapFrom(src => src.Dishes));
    }
}

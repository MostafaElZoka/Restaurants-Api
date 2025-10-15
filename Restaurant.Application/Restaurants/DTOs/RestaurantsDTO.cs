

using Restaurant.Application.Dishes.DTOs;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Restaurants.DTOs;

public class RestaurantsDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string Category { get; set; } = default!;
    public bool HasDelivery { get; set; }
    public string? City { get; set; }
    public string? Street { get; set; }
    public string? PostalCode { get; set; }
    public string? LogoSasUrl { get;  set; }
    public List<DishesDTO> Dishes { get; set; } = new List<DishesDTO>();

    //public static RestaurantsDTO? FromEntity(Restaurantt? r)//a method that maps dto instead of manually mapping each time (no longer need)
    //{
    //    if (r == null) return null;
    //    RestaurantsDTO dTO = new RestaurantsDTO()
    //    {
    //        Id = r.Id,
    //        Name = r.Name,
    //        Description = r.Description,
    //        HasDelivery = r.HasDelivery,
    //        Category = r.Category,
    //        City = r.Address?.City,
    //        PostalCode = r.Address?.PostalCode,
    //        Street = r.Address?.Street,
    //        Dishes = r.Dishes.Select(d =>DishesDTO.FromEntity(d)).ToList(),
    //    };
    //    return dTO;
    //}
}

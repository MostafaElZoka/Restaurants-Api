using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Dishes.DTOs;

public class DishesDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal Price { get; set; }
    public int? KiloCalories { get; set; }

    public static DishesDTO FromEntity(Dish d)
    {
        DishesDTO dishesDTO = new DishesDTO()
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description,
            Price = d.Price,
            KiloCalories = d.KiloCalories,
        };
        return dishesDTO;
    }
}

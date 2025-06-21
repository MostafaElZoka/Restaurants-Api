using MediatR;
using Restaurant.Application.Dishes.DTOs;
using Restaurant.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Dishes.Queries
{
    public class GetAllDishesQuery:IRequest<IEnumerable<DishesDTO>>
    {
        public int RestaurantId { get; set; }
    }
}

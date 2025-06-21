using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Inftastructure.Presistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Inftastructure.RestaurantsRepos
{
    internal class RestaurantRepository(RestaurantDbContext dbContext) : IRestaurantRepository
    {
        public async Task<int> Create(Restaurantt restaurant)
        {
            dbContext.Restaurants.Add(restaurant);
            await dbContext.SaveChangesAsync();
            return restaurant.Id;
        }

        public async Task Delete(Restaurantt Entity)
        {
            dbContext.Restaurants.Remove(Entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Restaurantt>> GetAllAsync()
        {
            return await dbContext.Restaurants.Include(d=>d.Dishes).ToListAsync();
        }

        public async Task<Restaurantt?> GetById(int id)
        {
            var restaurant = await dbContext.Restaurants.Include(d => d.Dishes).FirstOrDefaultAsync(r => r.Id == id);
            return restaurant;
        }

        public async Task Update(Restaurantt entity)
        {
            dbContext.Restaurants.Update(entity);
            await dbContext.SaveChangesAsync();
        }
    }
}

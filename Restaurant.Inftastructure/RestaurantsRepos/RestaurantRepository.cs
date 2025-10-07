using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Inftastructure.Presistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Inftastructure.RestaurantsRepos
{
    internal class RestaurantRepository(RestaurantDbContext dbContext) : IRestaurantRepository
    {
        public async Task<int> Create(Restaurantt restaurant)
        {
            dbContext.Restaurants.Add(restaurant);
            await SaveChanges();
            return restaurant.Id;
        }

        public async Task Delete(Restaurantt Entity)
        {
            dbContext.Restaurants.Remove(Entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<Restaurantt>> GetAllAsync()
        {
            return await dbContext.Restaurants.Include(d=>d.Dishes).ToListAsync();
        }

        public async Task<(IEnumerable<Restaurantt>, int)> GetAllMatching(string? searchPhrase,
                                                                          int pageSize,
                                                                          int pageNumber,
                                                                          string? sortBy,
                                                                          SortDirection? sortDirection)
        {
            var phraseToLower = searchPhrase?.ToLower();

            var baseQuery = dbContext.Restaurants
                .Where(r => searchPhrase == null ||            // if there's no search term then include all rows 
                (r.Name.ToLower().Contains(phraseToLower) || r.Description.ToLower().Contains(phraseToLower)));
             
            if(sortBy != null)
            {
                var sortingColumn = new Dictionary<string, Expression<Func<Restaurantt, object>>>
                {
                    { nameof(Restaurantt.Name), r => r.Name },
                    { nameof(Restaurantt.Description), r => r.Description },
                    { nameof(Restaurantt.Category), r => r.Category },
                };

                baseQuery =  sortDirection == SortDirection.Descending?
                         baseQuery.OrderByDescending(sortingColumn[sortBy]): baseQuery.OrderBy(sortingColumn[sortBy]);
            }

            var totalCount = await baseQuery.CountAsync(); //total count that we will display in the response

            var filteredQuery = await baseQuery //paginated result
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (filteredQuery, totalCount); //returning the tuple
        }

        public async Task<Restaurantt?> GetById(int id)
        {
            var restaurant = await dbContext.Restaurants.Include(d => d.Dishes).FirstOrDefaultAsync(r => r.Id == id);
            return restaurant;
        }

        public async Task SaveChanges()
        {
            await dbContext.SaveChangesAsync();
        }

        public async Task Update(Restaurantt entity)
        {
            dbContext.Restaurants.Update(entity);
            await SaveChanges();
        }
    }
}

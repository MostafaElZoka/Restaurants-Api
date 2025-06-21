
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Exceptions;
using Restaurant.Domain.Repositories;
using Restaurant.Inftastructure.Presistence;

namespace Restaurant.Inftastructure.DishesRepos;

internal class DishRepository(RestaurantDbContext dbContext,IRestaurantRepository restaurantRepository) : IDishReposatory
{
    public async Task<int> Create(Dish dish)
    {
        dbContext.Dishes.Add(dish);
        await dbContext.SaveChangesAsync();
        return dish.Id; 
    }

    public async Task Delete(Dish dish)
    {
        dbContext.Dishes.Remove(dish);
       await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Dish>> GetAll(int  restaurantid)
    {
        var restaurant = await restaurantRepository.GetById(restaurantid);
        if (restaurant == null)
        {
            throw new NotFoundExceptionHandler(nameof(restaurant),restaurantid.ToString());
        }
        var dishes = restaurant.Dishes;

        return dishes;
    }

    public async Task<Dish> GetById(int DishId, int RestaurantId)
    {
        var restaurant = await restaurantRepository.GetById(RestaurantId);
        if (restaurant == null)
        {
            throw new NotFoundExceptionHandler(nameof(restaurant), RestaurantId.ToString());
        }
        var dish = restaurant.Dishes.FirstOrDefault(d =>  d.Id == DishId);
        if (dish == null)
        {
            throw new NotFoundExceptionHandler(nameof(dish), DishId.ToString());
        }
        return dish;
    }

    public async Task Update(Dish dish)
    {
        dbContext.Dishes.Update(dish);
        await dbContext.SaveChangesAsync();
    }
}

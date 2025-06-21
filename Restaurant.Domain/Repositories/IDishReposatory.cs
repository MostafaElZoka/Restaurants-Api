

using Restaurant.Domain.Entities;

namespace Restaurant.Domain.Repositories
{
    public interface IDishReposatory
    {
        public Task<int> Create(Dish dish);
        public Task<IEnumerable<Dish>> GetAll(int restaurantid);
        public Task<Dish> GetById(int DishId,int RestaurantId);
        public Task Delete(Dish dish);
        public Task Update(Dish dish);
    }
}

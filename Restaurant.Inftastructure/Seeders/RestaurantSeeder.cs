
using Restaurant.Domain.Entities;
using Restaurant.Inftastructure.Presistence;

namespace Restaurant.Inftastructure.Seeders;

internal class RestaurantSeeder(RestaurantDbContext db): IRestaurantSeeder
{
    public async Task Seed()
    {
        if(await db.Database.CanConnectAsync())//if i am connected to the database
        {
            if(!db.Restaurants.Any())//if there is no data
            {
                var restaurants = GetRestaurants();
                db.Restaurants.AddRange(restaurants);
                await db.SaveChangesAsync();
            }
        }
    }

    private IEnumerable<Restaurantt> GetRestaurants()
    {
        List<Restaurantt> restaurants = [
        new Restaurantt
        {
            
            Name = "The Burger Joint",
            Description = "Best burgers in town.",
            Category = "Fast Food",
            HasDelivery = true,
            ContactEmail = "contact@burgerjoint.com",
            ContactNumber = "555-123-4567",
            Address = new Address
            {
                City = "New York",
                Street = "123 Main St",
                PostalCode = "10001"
            },
            Dishes =
            [
                new Dish { Name = "Classic Burger", Description = "Beef patty with lettuce, tomato, and cheese.", Price = 9.99m },
                new Dish { Name = "Fries", Description = "Crispy golden fries.", Price = 3.49m }
            ]
        },
        new Restaurantt
        {
            Name = "Sushi Zen",
            Description = "Fresh and authentic Japanese sushi.",
            Category = "Japanese",
            HasDelivery = false,
            ContactEmail = "info@sushizen.com",
            ContactNumber = "555-987-6543",
            Address = new Address
            {
                City = "San Francisco",
                Street = "456 Sushi Ave",
                PostalCode = "94105"
            },
            Dishes =
            [
                new Dish { Name = "Salmon Nigiri", Description = "Fresh salmon on vinegared rice.", Price = 5.99m },
                new Dish { Name = "California Roll", Description = "Crab, avocado, and cucumber roll.", Price = 7.49m }
            ]
        }
    ];
        return restaurants;
    }
}

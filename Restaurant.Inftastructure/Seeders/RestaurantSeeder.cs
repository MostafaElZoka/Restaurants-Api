
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Inftastructure.Presistence;

namespace Restaurant.Inftastructure.Seeders;

internal class RestaurantSeeder(RestaurantDbContext db): IRestaurantSeeder
{
    public async Task Seed()
    {
        
        if(db.Database.GetPendingMigrations().Any())
        {
            await db.Database.MigrateAsync();
        }
        if(await db.Database.CanConnectAsync())//if i am connected to the database
        {
            if(!db.Restaurants.Any())//if there is no data
            {
                var restaurants = GetRestaurants();
                db.Restaurants.AddRange(restaurants);
                await db.SaveChangesAsync();
            }

            if(!db.Roles.Any())
            {
                var roles = GetRoles();
                db.Roles.AddRange(roles);
                await db.SaveChangesAsync();

            }
        }
    }

    private IEnumerable<IdentityRole> GetRoles()
    {
        List<IdentityRole> roles =
                    [
                        new IdentityRole(UserRoles.Admin)
                        {
                            NormalizedName = UserRoles.Admin.ToUpper()// we must initaialize the NormalizedName column because it is the one that is use by the identity package to search and update etc
                        },
                        new IdentityRole(UserRoles.Owner)
                        {
                            NormalizedName = UserRoles.Owner.ToUpper()
                        },
                        new IdentityRole(UserRoles.User)
                        {
                            NormalizedName = UserRoles.User.ToUpper() 
                        }
                    ];
        return roles;
    }
    private IEnumerable<Restaurantt> GetRestaurants()
    {
        User owner = new User
        {
            Email = "seed@test.com",
            FullName= "seeding owner"
        };
        List<Restaurantt> restaurants = [
        new Restaurantt
        {
            Owner = owner,
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
            Owner = owner,
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

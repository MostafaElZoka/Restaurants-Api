using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
namespace Restaurant.Inftastructure.Presistence;

internal class RestaurantDbContext(DbContextOptions options):IdentityDbContext<User>(options)
{
    internal DbSet<Restaurantt> Restaurants { get; set; }
    internal DbSet<Dish> Dishes { get; set; }
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("");
    //}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    { 
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Restaurantt>()
            .OwnsOne(r => r.Address);

        modelBuilder.Entity<Restaurantt>()
            .HasMany(r => r.Dishes)
            .WithOne()
            .HasForeignKey(d => d.RestaurantId);
    }
}

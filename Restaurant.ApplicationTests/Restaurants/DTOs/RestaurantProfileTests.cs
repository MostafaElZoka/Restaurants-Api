using AutoMapper;
using FluentAssertions;
using Restaurant.Application.Restaurants.Commands.Create_Restaurant;
using Restaurant.Application.Restaurants.Commands.Update_Restaurant;
using Restaurant.Domain.Entities;
using Xunit;

namespace Restaurant.Application.Restaurants.DTOs.Tests;

public class RestaurantProfileTests
{
    IMapper _mapper;
    public RestaurantProfileTests()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<RestaurantProfile>();
        });

        _mapper = configuration.CreateMapper();
    }
    [Fact()]
    public void CreateMap_FromRestaurantToRestaurantDto_MapsCorrectly()
    {
        //arrange
        var restaurant = new Restaurantt
        {
            Id = 1,
            Name = "Tasty Bites",
            Description = "Cozy family restaurant serving local dishes.",
            Category = "Family",
            HasDelivery = true,
            ContactEmail = "info@tastybites.com",
            ContactNumber = "123-456-7890",
            Address = new Address
            {
                City = "Springfield",
                Street = "123 Main St",
                PostalCode = "12345"
            }
        };

        //act
        var dto = _mapper.Map<RestaurantsDTO>(restaurant);

        //assert
        restaurant.Id.Should().Be(1);
        restaurant.Name.Should().Be("Tasty Bites");
        restaurant.Description.Should().Be("Cozy family restaurant serving local dishes.");
        restaurant.Category.Should().Be("Family");
        restaurant.HasDelivery.Should().BeTrue();
        restaurant.ContactEmail.Should().Be("info@tastybites.com");
        restaurant.ContactNumber.Should().Be("123-456-7890");

        restaurant.Address.Should().NotBeNull();
        restaurant.Address!.City.Should().Be("Springfield");
        restaurant.Address.Street.Should().Be("123 Main St");
        restaurant.Address.PostalCode.Should().Be("12345");
    }

    [Fact()]
    public void CreateMap_FromCreateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        //arrange
        var command = new CreateRestaurantCommand
        {
            Name = "Quick Lunch",
            Description = "Affordable lunch meals.",
            Category = "Fast Food",
            HasDelivery = false
        };

        //act
        var restaurant = _mapper.Map<Restaurantt>(command);

        //assert
        restaurant.Name.Should().Be("Quick Lunch");
        restaurant.Description.Should().Be("Affordable lunch meals.");
        restaurant.Category.Should().Be("Fast Food");
        restaurant.HasDelivery.Should().BeFalse();
    }
    [Fact()]
    public void CreateMap_FromUpdateRestaurantCommandToRestaurant_MapsCorrectly()
    {
        //arrange
        var command = new UpdateRestaurantCommand
        {
            Id = 123,
            Name = "Quick Lunch",
            Description = "Affordable lunch meals.",
            HasDelivery = false
        };

        //act
        var restaurant = _mapper.Map<Restaurantt>(command);

        //assert
        restaurant.Name.Should().Be("Quick Lunch");
        restaurant.Description.Should().Be("Affordable lunch meals.");
        restaurant.Id.Should().Be(123);
        restaurant.HasDelivery.Should().BeFalse();
    }
}
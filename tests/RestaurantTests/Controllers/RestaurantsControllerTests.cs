using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Restaurant.Application.Restaurants.DTOs;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using Restaurant.Inftastructure.Seeders;
using RestaurantTests.Controllers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace Restaurant.Controllers.Tests
{
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _applicationFactory;
        private readonly Mock<IRestaurantRepository> _restaurantRepositoryMock = new();
        private readonly Mock<IRestaurantSeeder> _restaurantSeederMock = new();
        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            _applicationFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>(); //injecting the fake policy evaluator

                    services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantRepository), _ => _restaurantRepositoryMock.Object));

                    services.Replace(ServiceDescriptor.Scoped(typeof(IRestaurantSeeder), _ => _restaurantSeederMock.Object));
                });
            });
        }

        [Fact()]
        public async Task GetAll_ForValidRequest_ShouldReturn200()
        {
            //arrange 
            var client = _applicationFactory.CreateClient();

            //act
            var result = await client.GetAsync("api/Restaurants?pageNumber=1&pageSize=5");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Fact()]
        public async Task GetAll_ForInValidRequest_ShouldReturn400()
        {
            //arrange 
            var client = _applicationFactory.CreateClient();

            //act
            var result = await client.GetAsync("api/Restaurants");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.BadRequest);
        }

        [Fact()]
        public async Task GetById_ForInValidIdRequest_ShouldReturnNotFound() //we have to make a fake policy evaluator in order to skip authorization for this endpoint
        {
            //arrange 
            var id = 123;
            _restaurantRepositoryMock.Setup(m => m.GetById(id)).ReturnsAsync((Restaurantt?) null); //mocking the repository to return null for the given id
            var client = _applicationFactory.CreateClient();

            //act
            var result = await client.GetAsync($"api/Restaurants/{id}");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }

        [Fact()]
        public async Task GetById_ForValidIdRequest_ShouldReturnOk() //we have to make a fake policy evaluator in order to skip authorization for this endpoint
        {
            //arrange 
            var client = _applicationFactory.CreateClient();
            var id = 1;
            var restaurant = new Restaurantt()
            {
                Id = id,
                Name = "Test",
                Description = "Test",
            };
            _restaurantRepositoryMock.Setup(m => m.GetById(id)).ReturnsAsync(restaurant);

            //act
            var result = await client.GetAsync($"api/Restaurants/{id}");
            var restaurantDto = await result.Content.ReadFromJsonAsync<RestaurantsDTO>();

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            restaurantDto.Should().NotBeNull();
            restaurantDto.Name.Should().Be("Test");
            restaurantDto.Description.Should().Be("Test");
        }
    }
}
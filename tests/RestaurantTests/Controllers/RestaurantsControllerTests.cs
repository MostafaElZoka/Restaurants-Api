using FluentAssertions;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using RestaurantTests.Controllers;
using System.Threading.Tasks;
using Xunit;

namespace Restaurant.Controllers.Tests
{
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _applicationFactory;
        public RestaurantsControllerTests(WebApplicationFactory<Program> factory)
        {
            _applicationFactory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddSingleton<IPolicyEvaluator, FakePolicyEvaluator>(); //injecting the fake policy evaluator
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
            var client = _applicationFactory.CreateClient();
            var id = 123;

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

            //act
            var result = await client.GetAsync($"api/Restaurants/{id}");

            //assert
            result.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }
    }
}
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Threading.Tasks;
using Xunit;

namespace Restaurant.Controllers.Tests
{
    public class RestaurantsControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _applicationFactory;
        public RestaurantsControllerTests()
        {
            _applicationFactory = new WebApplicationFactory<Program>();
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
    }
}
using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurant.Application.UserInfo;
using Restaurant.Domain.Constants;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Repositories;
using System.Threading.Tasks;
using Xunit;

namespace Restaurant.Application.Restaurants.Commands.Create_Restaurant.Tests
{
    public class CreateRestaurantCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ReturnsCreatedRestaurantId()
        {
            //arrange
            var mapperMock = new Mock<IMapper>();
            var userContextMock = new Mock<IUserContext>();
            var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

            var command = new CreateRestaurantCommand();
            var restaurant = new Restaurantt();


            mapperMock.Setup(m => m.Map<Restaurantt>(command)).Returns(restaurant);

            var currentUser = new CurrentUser("id", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);
            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            restaurantRepositoryMock.Setup(r => r.Create(restaurant)).ReturnsAsync(1);

            var commandHandler = new CreateRestaurantCommandHandler(mapperMock.Object, restaurantRepositoryMock.Object, loggerMock.Object, userContextMock.Object);

            //act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            //assert
            result.Should().Be(1);
            restaurant.OwnerId.Should().Be("id");
            restaurantRepositoryMock.Verify(r => r.Create(restaurant), Times.Once);
        }
    }
}
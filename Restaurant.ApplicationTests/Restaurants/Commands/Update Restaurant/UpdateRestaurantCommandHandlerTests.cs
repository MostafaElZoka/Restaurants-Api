using Xunit;
using AutoMapper;
using Moq;
using Restaurant.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Restaurant.Domain.Interfaces;
using Restaurant.Domain.Entities;
using System.Threading.Tasks;
using FluentAssertions;
using Restaurant.Domain.Exceptions;

namespace Restaurant.Application.Restaurants.Commands.Update_Restaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        
        private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
        private readonly Mock<IRestaurantRepository> _restaurantsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IRestaurantAuthorizationServices> _restaurantAuthorizationServiceMock;

        private readonly UpdateRestaurantCommandHandler _handler;

        public UpdateRestaurantCommandHandlerTests()
        {
            _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
            _mapperMock = new Mock<IMapper>();
            _restaurantAuthorizationServiceMock = new Mock<IRestaurantAuthorizationServices>();
            _restaurantsRepositoryMock = new Mock<IRestaurantRepository>();

            _handler = new UpdateRestaurantCommandHandler(
                _restaurantsRepositoryMock.Object,
                _mapperMock.Object,
                _loggerMock.Object,
                _restaurantAuthorizationServiceMock.Object);
        }
        [Fact()]
        public async Task Handle_ForValidCommand_ShouldUpdateRestaurant()
        {
            //arrange
            var restaurantId = 1;

            var command = new UpdateRestaurantCommand
            {
                Id = restaurantId,
                Name = "new Test",
                Description = "zzzz",
                HasDelivery = true,
            };

            var restaurant = new Restaurantt
            {
                Id = restaurantId,
                Name = "old name",
                Description = "yyyy",
                HasDelivery = false,
            };

            _mapperMock.Setup(m => m.Map(command, restaurant)).Returns(restaurant);

            _restaurantsRepositoryMock.Setup(r => r.GetById(restaurantId)).ReturnsAsync(restaurant);

            _restaurantAuthorizationServiceMock.Setup(r => r.Authorize(restaurant, Domain.Constants.ResourceOperations.Update)).Returns(true);

            //act
            await _handler.Handle(command, CancellationToken.None);

            //assert

            _restaurantsRepositoryMock.Verify(r => r.Update(restaurant), Times.Once);
            _mapperMock.Verify(m => m.Map(command, restaurant), Times.Once);    
        }

        [Fact()]
        public async Task Handle_ForInValidCommand_ShouldThrowNotFoundException()
        {
            //arrange
            var restaurantId = 2;

            var command = new UpdateRestaurantCommand
            {
                Id = restaurantId,
            };

            _restaurantsRepositoryMock.Setup(r => r.GetById(restaurantId)).ReturnsAsync((Restaurantt?)null);

            //act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<NotFoundExceptionHandler>()
                .WithMessage("Restaurantt with id : 2 doesn't exist");
        }

        [Fact()]
        public async Task Handle_UnAuthorizedUser_ShouldThrowUnAuthorizedException()
        {
            var restaurantId = 1;

            var command = new UpdateRestaurantCommand
            {
                Id = restaurantId
            };

            var restaurant = new Restaurantt
            {
                Id = restaurantId,
            };

            _restaurantsRepositoryMock.Setup(r => r.GetById(restaurantId)).ReturnsAsync(restaurant);

            _restaurantAuthorizationServiceMock.Setup(r => r.Authorize(restaurant, Domain.Constants.ResourceOperations.Update))
                .Returns(false);

            //act
            Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

            //assert
            await act.Should().ThrowAsync<ForbidException>();        }
    }
}
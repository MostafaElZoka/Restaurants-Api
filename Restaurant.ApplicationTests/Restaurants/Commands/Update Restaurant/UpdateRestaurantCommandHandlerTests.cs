using Xunit;
using Restaurant.Application.Restaurants.Commands.Update_Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Restaurant.Application.Restaurants.Commands.Create_Restaurant;
using Restaurant.Application.UserInfo;
using Restaurant.Domain.Repositories;

namespace Restaurant.Application.Restaurants.Commands.Update_Restaurant.Tests
{
    public class UpdateRestaurantCommandHandlerTests
    {
        [Fact()]
        public void Handle_ForValidCommand_ReturnsCreatedRestaurantId()
        {
            Xunit.Assert.            //arrange
            var mapperMock = new Mock<IMapper>();
            var userContextMock = new Mock<IUserContext>();
            var restaurantRepositoryMock = new Mock<IRestaurantRepository>();
            var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>(); Fail("This test needs an implementation");
        }
    }
}
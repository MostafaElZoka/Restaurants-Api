using FluentAssertions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Moq;
using Restaurant.Application.UserInfo;
using Restaurant.Domain.Entities;
using Restaurant.Inftastructure.Authorization.Requirments;
using Xunit;

namespace Restaurant.Infrastructure.Authorization.Requirements.Tests
{
    public class OwnsAtLeast2RequirementHandlerTests
    {
        [Fact]
        public async Task HandleRequirementAsync_Should_Succeed_When_User_Owns_At_Least_2_Restaurants()
        {
            // Arrange
            //var loggerMock = new Mock<ILogger<OwnsAtLeast2RequirmentHandler>>();
            var userContextMock = new Mock<IUserContext>();

            var userId = "1";
            var currentUser = new CurrentUser(userId, "test@test.com", [], null, null);
            userContextMock.Setup(u => u.GetCurrentUser()).Returns(currentUser);

            var user = new User
            {
                Id = userId,
                RestaurantsOwned = new List<Restaurantt>
                {
                    new() { Id = 1, Name = "A" },
                    new() { Id = 2, Name = "B" }
                }
            };

            // Create an IQueryable<User>
            var users = new List<User> { user }.AsQueryable();

            var userManagerMock = CreateMockUserManager(users);

            var requirement = new OwnsAtLeast2Requirment();
            var context = new AuthorizationHandlerContext([requirement], null, null);

            var handler = new OwnsAtLeast2RequirmentHandler(
                null,
                userContextMock.Object,
                userManagerMock.Object);

            // Act
            await handler.HandleAsync(context);

            // Assert
            context.HasSucceeded.Should().BeTrue();
        }

        private static Mock<UserManager<User>> CreateMockUserManager(IQueryable<User> users)
        {
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(
                store.Object,
                null, null, null, null, null, null, null, null);

            userManager.Setup(x => x.Users).Returns(users);

            return userManager;
        }
    }
}

using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Restaurant.Domain.Constants;
using System.Security.Claims;
using Xunit;

namespace Restaurant.Application.UserInfo.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUserTest_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            ///arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>(); //mocking the httpcontextaccessor dependancy for testing
            var dob = new DateOnly(1990, 12, 12);
            var claims = new List<Claim>()
            {
                new(ClaimTypes.NameIdentifier, "1"),
                new(ClaimTypes.Email, "test@test.com"),
                new("Nationality", "Egyptian"),
                new("DateOfBirth",dob.ToString("yyyy-MM-dd")),
                new(ClaimTypes.Role, UserRoles.Admin),
                new(ClaimTypes.Role, UserRoles.User)
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            httpContextAccessorMock.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContext(httpContextAccessorMock.Object);

            //act
            var currentUser = userContext.GetCurrentUser();

            //assert
            currentUser.Should().NotBeNull();
            currentUser.Id.Should().Be("1");
            currentUser.Email.Should().Be("test@test.com");
            currentUser.Nationality.Should().Be("Egyptian");
            currentUser.DateOfBirth.Should().Be(dob);
            currentUser.Roles.Should().ContainInOrder(UserRoles.Admin, UserRoles.User);
        }


        [Fact()]
        public void GetCurrentUserTest_WithUserContextNotPresent_ShouldThrowException()
        {
            ///arrange
            var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            httpContextAccessorMock.Setup(x=>x.HttpContext).Returns((HttpContext)null);

            var userContext = new UserContext(httpContextAccessorMock.Object);

            //act
            Action action = () => userContext.GetCurrentUser();

            //assert
            action.Should().Throw<InvalidOperationException>().WithMessage("User context is not present");
        }
    }
}
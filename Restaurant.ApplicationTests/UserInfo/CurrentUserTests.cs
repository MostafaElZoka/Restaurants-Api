using FluentAssertions;
using Restaurant.Domain.Constants;
using Xunit;

namespace Restaurant.Application.UserInfo.Tests
{
    public class CurrentUserTests
    {
        [Theory()]
        [InlineData(UserRoles.Admin)]
        [InlineData(UserRoles.User)]
        public void IsInRoleTest_WithMatchingRole_ShouldBeTrue(string roleName)
        {
            //arrange
            var user = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            var result = user.IsInRole(roleName);

            //assert
            result.Should().BeTrue();
        }

        [Fact()]
        public void IsInRoleTest_WithNoMatchingRole_ShouldBeFalse()
        {
            //arrange
            var user = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            var result = user.IsInRole(UserRoles.Owner);

            //assert
            result.Should().BeFalse();
        }

        [Fact()]
        public void IsInRoleTest_WithNoMatchingRoleCase_ShouldBeFalse()
        {
            //arrange
            var user = new CurrentUser("1", "test@test.com", [UserRoles.Admin, UserRoles.User], null, null);

            //act
            var result = user.IsInRole(UserRoles.Admin.ToLower());

            //assert
            result.Should().BeFalse();
        }
    }
}
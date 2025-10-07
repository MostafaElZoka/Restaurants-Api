using FluentAssertions;
using FluentValidation.TestHelper;
using Restaurant.Application.Restaurants.Commands.Create_Restaurant;
using Xunit;

namespace Restaurant.Application.Validators.Tests;

public class CreateRestaurantDTOValidationsTests
{
    [Fact()]
    public void CreateRestaurantDTOValidationsTest_WithValidData_ShouldNotHaveValidationErrors()
    {
        //arrange
        var restaurant = new CreateRestaurantCommand()
        {
            Category = "Italian",
            Name = "Test",
            ContactEmail = "Test@test.com",
            PostalCode = "12-345",
            Description = "Test",  
        };

        //act
        var validator = new CreateRestaurantDTOValidations();

        var result = validator.TestValidate(restaurant);

        //assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact()]
    public void CreateRestaurantDTOValidationsTest_WithInvlidData_ShouldHaveValidationErrors()
    {
        //arrange
        var restaurant = new CreateRestaurantCommand()
        {
            Category = "Egyptian",
            Name = "Tt",
            ContactEmail = "Tetest.com",
            PostalCode = "125",
            Description = "",
        };

        //act
        var validator = new CreateRestaurantDTOValidations();

        var result = validator.TestValidate(restaurant);

        //assert
        result.ShouldHaveValidationErrorFor(x => x.PostalCode);
        result.ShouldHaveValidationErrorFor(x => x.Name);
        result.ShouldHaveValidationErrorFor(x => x.ContactEmail);
        result.ShouldHaveValidationErrorFor(x => x.Description);
        result.ShouldHaveValidationErrorFor(x => x.Category);
    }

    [Theory]
    [InlineData("Fast Food")]
    [InlineData("Japanese")]
    [InlineData("Italian")]
    [InlineData("American")]
    public void ValidatorForValidCategory_ShouldNotHaveValidationErrors(string category)
    {
        //arrange
        var restaurant = new CreateRestaurantCommand()
        {
            Category = category,
        };

        //act
        var validator = new CreateRestaurantDTOValidations();

        var result = validator.TestValidate(restaurant);

        //assert
        result.ShouldNotHaveValidationErrorFor(c => c.Category);
    }
}
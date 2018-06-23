using System;
using TimeRegistrationDemo.Services.Dtos.RegisterHolidayRequest;
using TimeRegistrationDemo.Services.Validation.DtoValidators;
using Xunit;

namespace TimeRegistrationDemo.Services.Test
{
    public class RegisterInputDtoValidatorTests
    {
        [Fact]
        public void Given_DefaultItem_When_Validate_Then_Fail()
        {
            // Arrange
            var input = new RegisterHolidayRequestInputDto(
                default(DateTime),
                default(DateTime),
                default(string),
                default(string),
                default(long));
            var sut = new RegisterHolidayRequestInputDtoValidator(null, null);

            // Act
            var actual = sut.Validate(input);

            // Assert
            Assert.False(actual.IsValid);
        }
    }
}

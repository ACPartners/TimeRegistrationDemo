using Moq;
using System;
using System.Collections.Generic;
using TimeRegistrationDemo.Repositories.Interfaces;
using TimeRegistrationDemo.Services.Dtos.RegisterHolidayRequest;
using TimeRegistrationDemo.Services.Validation.DtoValidators;
using Xunit;

namespace TimeRegistrationDemo.Services.Test
{
    public class RegisterInputDtoValidatorTests
    {
        [Fact]
        public void Given_DefaultItem_When_Validate_Then_IsValidFalse()
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

        [Fact]
        public void Given_DefaultItem_When_Validate_Then_ThreeErrorMessages()
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
            Assert.Equal(3, actual.Errors.Count);
        }

        [Fact]
        public void Given_DefaultRequestWithHolidayTypeAndNullHolidayTypeRepository_When_Validate_Then_ThrowsNullReference()
        {
            // Arrange
            var input = new RegisterHolidayRequestInputDto(
                default(DateTime),
                default(DateTime),
                default(string),
                "N",
                default(long));
            var sut = new RegisterHolidayRequestInputDtoValidator(null, null);

            // Act+Assert
            Assert.Throws<NullReferenceException>(() => sut.Validate(input));
        }

        [Fact]
        public void Given_DefaultRequestWithHolidayTypeAndMockHolidayTypeRepository_When_Validate_Then_IsValidFalseWith2Errors()
        {
            // Arrange
            var input = new RegisterHolidayRequestInputDto(
                default(DateTime),
                default(DateTime),
                default(string),
                "N",
                default(long));
            var holidayTypeRepoMock = new Mock<IHolidayTypeRepository>();
            holidayTypeRepoMock
                .Setup(x => x.ExistsByKey(It.IsAny<string>()))
                .Returns(true);
            var sut = new RegisterHolidayRequestInputDtoValidator(
                holidayTypeRepoMock.Object,
                null);

            // Act
            var actual = sut.Validate(input);

            // Assert
            Assert.False(actual.IsValid);
            Assert.Equal(2, actual.Errors.Count);
            holidayTypeRepoMock.Verify(x => x.ExistsByKey(It.IsAny<string>()), Times.Exactly(1));
        }

        [Theory]
        [InlineData("N", true)]
        [InlineData("F", false)]
        public void Given_HolidayTypeAndOtherwiseValidRequest_When_Validate_Then_ExpectedOutcome(string holidayType, bool expected)
        {
            // Arrange
            var input = new RegisterHolidayRequestInputDto(
                new DateTime(2018, 12, 23),
                new DateTime(2018, 12, 31),
                null,
                holidayType,
                0L);

            var holidayTypeRepoMock = new Mock<IHolidayTypeRepository>();
            holidayTypeRepoMock
                .Setup(x => x.ExistsByKey(It.IsAny<string>()))
                .Returns(false);
            holidayTypeRepoMock
                .Setup(x => x.ExistsByKey(It.Is<string>(s => s == "N")))
                .Returns(true);
            var holidayRequestRepoMock = new Mock<IHolidayRequestRepository>();
            holidayRequestRepoMock
                .Setup(x => x.ExistsByToAndFrom(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<long>()))
                .Returns(false);

            var sut = new RegisterHolidayRequestInputDtoValidator(
                holidayTypeRepoMock.Object,
                holidayRequestRepoMock.Object);

            // Act
            var actual = sut.Validate(input);

            // Assert
            Assert.Equal(expected, actual.IsValid);
        }

        [Theory]
        [MemberData(nameof(CreateComplexData))]
        public void Given_HolidayRequest_When_Validate_Then_ExpectedOutcome(
            RegisterHolidayRequestInputDto input, 
            bool holidayRequestRepoResult,
            bool isValid, 
            List<string> errors)
        {
            errors = errors ?? new List<string>();
            // Arrange
            var holidayTypeRepoMock = new Mock<IHolidayTypeRepository>();
            holidayTypeRepoMock
                .Setup(x => x.ExistsByKey(It.IsAny<string>()))
                .Returns(false);
            holidayTypeRepoMock
                .Setup(x => x.ExistsByKey(It.Is<string>(s => s == "N")))
                .Returns(true);
            var holidayRequestRepoMock = new Mock<IHolidayRequestRepository>();
            holidayRequestRepoMock
                .Setup(x => x.ExistsByToAndFrom(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<long>()))
                .Returns(holidayRequestRepoResult);

            var sut = new RegisterHolidayRequestInputDtoValidator(
                holidayTypeRepoMock.Object,
                holidayRequestRepoMock.Object);

            // Act
            var actual = sut.Validate(input);

            // Assert
            Assert.Equal(isValid, actual.IsValid);
            Assert.Equal(errors.Count, actual.Errors.Count);
            for (var i = 0; i < errors.Count; i++)
            {
                Assert.Equal(errors[i], actual.Errors[i].ErrorMessage);
            }
        }
        public static TheoryData<RegisterHolidayRequestInputDto, bool, bool, List<string>> CreateComplexData()
        {
            var result = new TheoryData<RegisterHolidayRequestInputDto, bool, bool, List<string>>();

            result.Add(
                new RegisterHolidayRequestInputDto(
                    new DateTime(2018, 12, 23),
                    new DateTime(2018, 12, 31),
                    null,
                    "N",
                    0L),
                false,
                true,
                null);

            result.Add(
                new RegisterHolidayRequestInputDto(
                    default(DateTime),
                    new DateTime(2018, 12, 31),
                    null,
                    "N",
                    0L),
                false,
                false,
                new List<string>{
                    "From date is required."
                });

            result.Add(
                new RegisterHolidayRequestInputDto(
                    new DateTime(2018, 12, 23),
                    default(DateTime),
                    null,
                    "N",
                    0L),
                false,
                false,
                new List<string>{
                    "To date is required."
                });


            result.Add(
                new RegisterHolidayRequestInputDto(
                    default(DateTime),
                    default(DateTime),
                    null,
                    "N",
                    0L),
                false,
                false,
                new List<string>{
                    "From date is required.",
                    "To date is required."
                });

            result.Add(
                new RegisterHolidayRequestInputDto(
                    new DateTime(2018, 12, 23),
                    new DateTime(2018, 12, 31),
                    null,
                    "Daedalus",
                    0L),
                false,
                false,
                new List<string>{
                    "HolidayType is not a valid."
                });

            result.Add(
                new RegisterHolidayRequestInputDto(
                    new DateTime(2018, 12, 23),
                    new DateTime(2018, 12, 31),
                    new string('x', 201),
                    "N",
                    0L),
                false,
                false,
                new List<string>{
                    "The length of 'Remarks' must be 200 characters or fewer. You entered 201 characters."
                });

            result.Add(
                new RegisterHolidayRequestInputDto(
                    new DateTime(2018, 12, 31),
                    new DateTime(2018, 12, 23),
                    null,
                    "N",
                    0L),
                false,
                false,
                new List<string>{
                    "From date must be before To date."
                });

            result.Add(
                new RegisterHolidayRequestInputDto(
                    new DateTime(2017, 12, 23),
                    new DateTime(2017, 12, 31),
                    null,
                    "N",
                    0L),
                false,
                false,
                new List<string>{
                    "From date must be before today."  // this message is wrong!
                });

            result.Add(
                new RegisterHolidayRequestInputDto(
                    new DateTime(2018, 12, 23),
                    new DateTime(2018, 12, 31),
                    null,
                    "N",
                    0L),
                true,
                false,
                new List<string>{
                    "This holiday period is already registered for you."
                });

            return result;
        }
    }
}

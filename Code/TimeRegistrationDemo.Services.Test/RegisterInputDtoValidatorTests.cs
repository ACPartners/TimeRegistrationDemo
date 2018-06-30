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
        public class RegisterHolidayRequestInputDtoBuilder
        {
            private DateTime _from;
            private DateTime _to;
            private string _remarks;
            private string _ht;

            public RegisterHolidayRequestInputDtoBuilder WithHolidayType(string holidayType)
            {
                _ht = holidayType;
                return this;
            }

            public RegisterHolidayRequestInputDtoBuilder WithOneWeekOffInTheFuture()
            {
                _from = DateTime.Now + TimeSpan.FromDays(7);
                _to = _from + TimeSpan.FromDays(7);
                return this;
            }

            public RegisterHolidayRequestInputDtoBuilder WithToDateBeforeFromDateInTheFuture()
            {
                _to = DateTime.Now + TimeSpan.FromDays(7);
                _from = _to + TimeSpan.FromDays(7);
                return this;
            }

            public RegisterHolidayRequestInputDtoBuilder WithOneWeekOffInThePast()
            {
                _from = DateTime.Now - TimeSpan.FromDays(700);
                _to = _from + TimeSpan.FromDays(7);
                return this;
            }

            public RegisterHolidayRequestInputDtoBuilder WithFromDateInTheFuture()
            {
                _from = DateTime.Now + TimeSpan.FromDays(7);
                return this;
            }

            public RegisterHolidayRequestInputDtoBuilder WithToDateInTheFuture()
            {
                _to = DateTime.Now + TimeSpan.FromDays(14);
                return this;
            }

            public RegisterHolidayRequestInputDtoBuilder WithRemarks(int length)
            {
                _remarks = new string('x', length);
                return this;
            }

            public RegisterHolidayRequestInputDto Build()
                => new RegisterHolidayRequestInputDto(
                    _from,
                    _to,
                    _remarks,
                    _ht,
                    0L);
        }

        [Fact]
        public void Given_DefaultItem_When_Validate_Then_IsValidFalse()
        {
            // Arrange
            var builder = new RegisterHolidayRequestInputDtoBuilder();
            var input = builder
                .Build();
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
            var builder = new RegisterHolidayRequestInputDtoBuilder();
            var input = builder
                .Build();
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
            var builder = new RegisterHolidayRequestInputDtoBuilder();
            var input = builder
                .WithHolidayType("N")
                .Build();            
            var sut = new RegisterHolidayRequestInputDtoValidator(null, null);

            // Act+Assert
            Assert.Throws<NullReferenceException>(() => sut.Validate(input));
        }

        [Fact]
        public void Given_DefaultRequestWithHolidayTypeAndMockHolidayTypeRepository_When_Validate_Then_IsValidFalseWith2Errors()
        {
            // Arrange
            var builder = new RegisterHolidayRequestInputDtoBuilder();
            var input = builder
                .WithHolidayType("N")
                .Build();            
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
            var builder = new RegisterHolidayRequestInputDtoBuilder();
            var input = builder
                .WithOneWeekOffInTheFuture()
                .WithHolidayType(holidayType)
                .Build();
            
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

            var builder = new RegisterHolidayRequestInputDtoBuilder();
            result.Add(
                builder
                    .WithOneWeekOffInTheFuture()
                    .WithHolidayType("N")
                    .Build(),
                false,
                true,
                null);

            builder = new RegisterHolidayRequestInputDtoBuilder();
            result.Add(
                builder
                    .WithToDateInTheFuture()
                    .WithHolidayType("N")
                    .Build(),
                false,
                false,
                new List<string>{
                    "From date is required."
                });

            builder = new RegisterHolidayRequestInputDtoBuilder();
            result.Add(
                builder
                    .WithFromDateInTheFuture()
                    .WithHolidayType("N")
                    .Build(),                
                false,
                false,
                new List<string>{
                    "To date is required."
                });

            builder = new RegisterHolidayRequestInputDtoBuilder();
            result.Add(
                builder
                    .WithHolidayType("N")
                    .Build(),
                false,
                false,
                new List<string>{
                    "From date is required.",
                    "To date is required."
                });

            builder = new RegisterHolidayRequestInputDtoBuilder();
            result.Add(
                builder
                    .WithOneWeekOffInTheFuture()
                    .WithHolidayType("Daedalus")
                    .Build(),               
                false,
                false,
                new List<string>{
                    "HolidayType is not a valid."
                });

            builder = new RegisterHolidayRequestInputDtoBuilder();
            result.Add(
                builder
                    .WithOneWeekOffInTheFuture()
                    .WithHolidayType("N")
                    .WithRemarks(201)
                    .Build(),
                false,
                false,
                new List<string>{
                    "The length of 'Remarks' must be 200 characters or fewer. You entered 201 characters."
                });

            builder = new RegisterHolidayRequestInputDtoBuilder();
            result.Add(
                builder
                    .WithToDateBeforeFromDateInTheFuture()
                    .WithHolidayType("N")
                    .Build(),                
                false,
                false,
                new List<string>{
                    "From date must be before To date."
                });

            builder = new RegisterHolidayRequestInputDtoBuilder();
            result.Add(
                builder
                    .WithOneWeekOffInThePast()
                    .WithHolidayType("N")
                    .Build(),
                false,
                false,
                new List<string>{
                    "From date must be before today."  // this message is wrong!
                });

            builder = new RegisterHolidayRequestInputDtoBuilder();
            result.Add(
                builder
                    .WithOneWeekOffInTheFuture()
                    .WithHolidayType("N")
                    .Build(),
                true,
                false,
                new List<string>{
                    "This holiday period is already registered for you."
                });

            return result;
        }
    }
}

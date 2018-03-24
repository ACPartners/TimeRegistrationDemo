using System;
using TimeRegistrationDemo.Services.Dtos.Shared;
using TimeRegistrationDemo.Services.Validation.ValidationResult;

namespace TimeRegistrationDemo.Services.Dtos.RegisterHolidayRequest
{
    public class RegisterHolidayRequestOutputDto
    {
        public TRValidationResult ValidationResult { get; }
        public HolidayRequestDto Result { get; }

        public RegisterHolidayRequestOutputDto(HolidayRequestDto result, TRValidationResult validationResult)
        {
            Result = result;
            ValidationResult = validationResult;
        }
    }
}
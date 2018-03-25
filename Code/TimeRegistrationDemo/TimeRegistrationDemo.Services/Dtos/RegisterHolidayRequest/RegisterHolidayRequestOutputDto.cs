using System;
using TimeRegistrationDemo.Services.Dtos.Shared;
using TimeRegistrationDemo.Services.Validation.ValidationResult;

namespace TimeRegistrationDemo.Services.Dtos.RegisterHolidayRequest
{
    public class RegisterHolidayRequestOutputDto
    {
        public TRValidationResult ValidationResult { get; }
        public HolidayRequestDto Result { get; }
        public bool IsSuccessful => ValidationResult == null;

        public RegisterHolidayRequestOutputDto(HolidayRequestDto result)
        {
            Result = result;
        }

        public RegisterHolidayRequestOutputDto(TRValidationResult validationResult)
        {
            ValidationResult = validationResult;
        }
    }
}
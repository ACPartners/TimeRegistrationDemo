using FluentValidation;
using System;
using TimeRegistrationDemo.Repositories.Interfaces;
using TimeRegistrationDemo.Services.Dtos.RegisterHolidayRequest;

namespace TimeRegistrationDemo.Services.Validation.DtoValidators
{
    public class RegisterHolidayRequestInputDtoValidator : AbstractValidator<RegisterHolidayRequestInputDto>
    {
        public RegisterHolidayRequestInputDtoValidator(
            IHolidayTypeRepository holidayTypeRepository,
            IHolidayRequestRepository holidayRequestRepository)
        {
            RuleFor(dto => dto.From)
                .NotEqual(DateTime.MinValue) //datetime is value type -> can never be null
                .WithMessage("From date is required.");

            RuleFor(dto => dto.To)
                .NotEqual(DateTime.MinValue) //datetime is value type -> can never be null
                .WithMessage("To date is required.");

            RuleFor(dto => dto.HolidayType)
                .NotNull();

            RuleFor(dto => dto.HolidayType)
                .Custom((holidayType, context) =>
                {
                    if (!string.IsNullOrEmpty(holidayType)
                        && !holidayTypeRepository.ExistsByKey(holidayType))
                    {
                        context.AddFailure("HolidayType is not a valid.");
                    }
                });

            RuleFor(dto => dto.Remarks)
                .MaximumLength(200);

            RuleFor(dto => dto)
                .Custom((dto, context) =>
                {
                    if (dto.From != DateTime.MinValue && dto.To != DateTime.MinValue)
                    {
                        if (dto.From > dto.To)
                        {
                            context.AddFailure("From date must be before To date.");
                        }

                        if (dto.From < DateTime.Today)
                        {
                            context.AddFailure("From date must be before today.");
                        }

                        if (!(dto.From > dto.To)
                            && !(dto.From < DateTime.Today)
                            && holidayRequestRepository.ExistsByToAndFrom(dto.From, dto.To, dto.UserId))
                        {
                            context.AddFailure("This holiday period is already registered for you.");
                        }
                    }
                });
        }
    }
}
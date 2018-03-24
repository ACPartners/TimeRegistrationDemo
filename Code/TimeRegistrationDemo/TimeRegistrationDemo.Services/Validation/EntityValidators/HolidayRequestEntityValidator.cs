using FluentValidation;
using System;
using TimeRegistrationDemo.Data.Entities;

namespace TimeRegistrationDemo.Services.Validation.EntityValidators
{
    public class HolidayRequestEntityValidator : AbstractValidator<HolidayRequestEntity>
    {
        public HolidayRequestEntityValidator()
        {
            RuleFor(hr => hr.From)
                .NotEqual(DateTime.MinValue) //datetime is value type -> can never be null
                .WithMessage("From date is required.");

            RuleFor(hr => hr.To)
                .NotEqual(DateTime.MinValue) //datetime is value type -> can never be null
                .WithMessage("To date is required.");

            RuleFor(hr => hr.HolidayType)
                .NotNull();

            RuleFor(hr => hr.User)
                .NotNull();

            //DisapprovedReason is required when IsApproved is false
            RuleFor(hr => hr.DisapprovedReason)
                .NotEmpty()
                .When(hr => hr.IsApproved.HasValue && !hr.IsApproved.Value);

            RuleFor(hr => hr.DisapprovedReason)
                .MaximumLength(200)
                .When(hr => hr.IsApproved.HasValue && !hr.IsApproved.Value);

            RuleFor(hr => hr.Remarks)
                .MaximumLength(200);

            RuleFor(hr => hr)
                .Custom((hr, context) =>
                {
                    if (hr.From != DateTime.MinValue && hr.To != DateTime.MinValue)
                    {
                        if (hr.From > hr.To)
                        {
                            context.AddFailure("From date must be before To date.");
                        }

                        if (hr.From < DateTime.Today)
                        {
                            context.AddFailure("From date must be before today.");
                        }
                    }
                });
        }
    }
}

//todo nog testen (dispprovereason in combinatie met isapproved)
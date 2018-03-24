using System.Linq;

namespace TimeRegistrationDemo.Services.Validation.ValidationResult
{
    public static class FluentValidationExtensions
    {
        public static TRValidationResult ToTRValidationResult(this FluentValidation.Results.ValidationResult fluentValidationResult)
        {
            return new TRValidationResult
            {
                Errors = fluentValidationResult.Errors.Select(
                    fvr => new TRValidationError
                    {
                        ErrorMessage = fvr.ErrorMessage
                    }).ToList()
            };
        }
    }
}

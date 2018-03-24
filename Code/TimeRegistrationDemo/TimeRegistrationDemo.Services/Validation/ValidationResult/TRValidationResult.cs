using System.Collections.Generic;
using System.Linq;

namespace TimeRegistrationDemo.Services.Validation.ValidationResult
{
    public class TRValidationResult
    {
        public bool IsValid => !Errors.Any();

        public IList<TRValidationError> Errors { get; set; }
    }
}

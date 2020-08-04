using System.Collections.Generic;
using DentalScheduler.Interfaces.UseCases.Validation;

namespace DentalScheduler.UseCases.Validation
{
    public class ValidationResult : IValidationResult
    {
        public bool IsValid { get; set; }

        public IList<IValidationError> Errors { get; set; }

        public string[] RuleSetsExecuted { get; set; }
    }
}
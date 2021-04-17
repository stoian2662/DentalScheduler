using System.Collections.Generic;
using DentalSystem.Boundaries.Infrastructure.Identity.Dto.Output;
using DentalSystem.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Infrastructure.Identity.Dto.Output
{
    public class AuthResult : IAuthResult
    {
        public AuthResult (bool succeeded, IEnumerable<IError> errors)
        {
            Errors = errors;
            Succeeded = succeeded;
        }

        public bool Succeeded { get; }

        public IEnumerable<IError> Errors { get; }
    }
}
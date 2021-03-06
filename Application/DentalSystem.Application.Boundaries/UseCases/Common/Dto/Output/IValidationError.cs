using System.Collections.Generic;
using DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output;

namespace DentalSystem.Application.Boundaries.UseCases.Common.Dto.Output
{
    public interface IValidationError : IError
    {
        string PropertyName { get; }

        IList<string> Errors { get; }
    }
}
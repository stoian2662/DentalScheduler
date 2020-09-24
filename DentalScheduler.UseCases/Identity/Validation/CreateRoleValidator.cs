using DentalScheduler.Interfaces.Models.Input;
using FluentValidation;

namespace DentalScheduler.UseCases.Identity.Validation
{
    public class CreateRoleValidator : AbstractValidator<ICreateRoleInput>
    {
        public CreateRoleValidator()
        {
            RuleFor(model => model.Name)
                .NotEmpty()
                .NotNull();
        }
    }
}
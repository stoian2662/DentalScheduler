using System.IO;

namespace DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Input
{
    public interface IUserProfileInput
    {
        byte[] Avatar { get; }

        string FirstName { get; }

        string LastName { get; }
    }
}
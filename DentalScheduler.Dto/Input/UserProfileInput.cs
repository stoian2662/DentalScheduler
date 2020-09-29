using DentalScheduler.Shared.Helpers.Extensions;
using DentalScheduler.Interfaces.Models.Input;
using Microsoft.AspNetCore.Http;

namespace DentalScheduler.DTO.Input
{
    public class UserProfileInput : IUserProfileInput
    {
        public IFormFile Avatar { get; set; }

        byte[] IUserProfileInput.Avatar => Avatar.ToArray();

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
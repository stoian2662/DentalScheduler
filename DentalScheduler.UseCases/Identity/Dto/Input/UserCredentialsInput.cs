using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;

namespace DentalScheduler.UseCases.Identity.Dto.Input
{
    public class UserCredentialsInput : IUserCredentialsInput
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
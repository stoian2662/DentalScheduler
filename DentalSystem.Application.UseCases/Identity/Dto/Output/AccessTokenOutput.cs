using DentalSystem.Application.Boundaries.UseCases.Identity.Dto.Output;

namespace DentalSystem.Application.UseCases.Identity.Dto.Output
{
    public class AccessTokenOutput : IAccessTokenOutput
    {
        public AccessTokenOutput()
        { }

        public AccessTokenOutput(string accessToken)
        {
            AccessToken = accessToken;
        }

        public string AccessToken { get; set; }
    }
}
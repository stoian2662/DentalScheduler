using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI.UseCases.Identity
{
    public static class AllRegistrations
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services)
            => services
                .RegisterCommands()
                .RegisterQueries()
                .RegisterValidation();
    }
}
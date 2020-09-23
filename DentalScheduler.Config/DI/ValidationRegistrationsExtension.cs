using DentalScheduler.Interfaces.Models.Input;
using DentalScheduler.Interfaces.UseCases.Common.Validation;
using DentalScheduler.UseCases.Common.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace DentalScheduler.Config.DI
{
    internal static class ValidationRegistrationsExtension
    {
        public static IServiceCollection RegisterValidationDependencies(this IServiceCollection services)
        {
            services.AddTransient<AbstractValidator<ILinkUserAndRoleInput>, LinkUserAndRoleValidator>();
            services.AddTransient<AbstractValidator<ICreateRoleInput>, CreateRoleValidator>();
            services.AddTransient<AbstractValidator<IUserCredentialsInput>, UserCredentialsValidator>();
            services.AddTransient<AbstractValidator<ITreatmentSessionInput>, TreatmentSessionValidator>();
            
            services.AddTransient(typeof(IApplicationValidator<>), typeof(ApplicationValidator<>));
            
            return services;
        }
    }
}
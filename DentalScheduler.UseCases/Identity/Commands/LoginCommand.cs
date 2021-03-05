﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DentalScheduler.UseCases.Identity.Dto.Output;
using DentalScheduler.UseCases.Common.Dto.Output;
using DentalScheduler.Entities.Identity;
using DentalScheduler.Interfaces.Infrastructure.Identity;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Input;
using DentalScheduler.Interfaces.UseCases.Identity.Dto.Output;
using DentalScheduler.Interfaces.UseCases.Common.Dto.Output;
using DentalScheduler.Interfaces.UseCases.Identity.Commands;
using DentalScheduler.Interfaces.UseCases.Common.Validation;
using DentalScheduler.UseCases.Common.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DentalScheduler.UseCases.Identity.Commands
{
    public class LoginCommand : ILoginCommand
    {
        public IConfiguration Config { get; }

        public IUserService<User> UserService { get; }

        public IApplicationValidator<IUserCredentialsInput> Validator { get; }

        public IJwtAuthManager JwtAuthManager { get; }

        public LoginCommand(
            IConfiguration config,
            IUserService<User> userService,
            IApplicationValidator<IUserCredentialsInput> validator,
            IJwtAuthManager jwtAuthManager)
        {
            Config = config;
            UserService = userService;
            Validator = validator;
            JwtAuthManager = jwtAuthManager;
        }

        public async Task<IResult<IAccessTokenOutput>> LoginAsync(IUserCredentialsInput userInput)
        {
            var validationResult = Validator.Validate(userInput);
            if (validationResult.Errors.Count > 0)
            {
                return new Result<IAccessTokenOutput>(validationResult.Errors);
            }

            var user = await UserService.FindByNameAsync(userInput.UserName);
            if (user == null)
            {
                validationResult.Errors.Add(
                    new ValidationError()
                    {
                        PropertyName = nameof(IUserCredentialsInput.UserName),
                        Errors = new [] { "User does not exist." }
                    }
                );
            }

            if (user != null && !(await UserService.CheckPasswordAsync(user, userInput.Password)))
            {
                validationResult.Errors.Add(
                    new ValidationError()
                    {
                        PropertyName = nameof(IUserCredentialsInput.Password),
                        Errors = new [] { "Invalid password." }
                    }
                );
            }

            if (validationResult.Errors.Count > 0)
            {
                return new Result<IAccessTokenOutput>(validationResult.Errors);
            }

            var roleName = (await UserService.GetRolesAsync(user)).FirstOrDefault();
            var tokenString = await JwtAuthManager.GenerateJwtAsync(userInput, roleName);

            return new Result<IAccessTokenOutput>(new AccessTokenOutput(tokenString));
        }
    }
}

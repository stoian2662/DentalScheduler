using System.Collections.Generic;
using DentalSystem.Application.UseCases.Common.Validation;
using DentalSystem.Application.UseCases.Identity.Dto.Output;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace DentalSystem.Presentation.Web.Api.Tests.Common.Steps
{
    public class ShouldReceiveLoginErrorsStep
    {
        private readonly ScenarioContext _scenarioContext;

        public ShouldReceiveLoginErrorsStep(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        public void ShouldReceiveLoginErrors()
        {
            var accessTokens = _scenarioContext
                .Get<List<AccessTokenOutput>>(LoginStep.AccessTokensLabel);
            var errors = _scenarioContext
                .Get<List<List<ValidationError>>>(LoginStep.ErrorsLabel);

            accessTokens.Should().BeEmpty();
            errors.Should().OnlyContain(el => el.Count > 0);
        }
    }
}
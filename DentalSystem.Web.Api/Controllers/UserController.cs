using System;
using System.Threading.Tasks;
using DentalSystem.UseCases.Identity.Dto.Input;
using DentalSystem.Interfaces.UseCases.Identity.Dto.Output;
using DentalSystem.Interfaces.UseCases.Identity.Commands;
using DentalSystem.Interfaces.UseCases.Identity.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace DentalSystem.Web.Api.Controllers
{
    /// <summary>
    /// User.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class UserController : BaseApiController
    {
        /// <summary>
        /// Creates User Controller.
        /// </summary>
        /// <param name="getUserProfileQuery"></param>
        /// <param name="updateProfileCommand"></param>
        public UserController(
            Lazy<IGetUserProfileQuery> getUserProfileQuery,
            Lazy<IUpdateProfileCommand> updateProfileCommand)
        {
            GetUserProfileQuery = getUserProfileQuery;
            UpdateProfileCommand = updateProfileCommand;
        }

        private Lazy<IGetUserProfileQuery> GetUserProfileQuery { get; }

        private Lazy<IUpdateProfileCommand> UpdateProfileCommand { get; }

        /// <summary>
        /// Gets logged in user avatar.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns user avatar</response>
        [HttpGet]
        [Route("avatar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAvatar()
        {
            var result = await GetUserProfileQuery.Value.ExecuteAsync();

            return File(result.Avatar, "image/jpeg");
        }

        /// <summary>
        /// Gets logged in user's profile.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns user profile</response>
        [HttpGet]
        [Route("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IUserProfileOutput> GetProfile()
        {
            var result = await GetUserProfileQuery.Value.ExecuteAsync();

            return result;
        }

        /// <summary>
        /// Updates logged in user profile.
        /// </summary>
        /// <param name="input">User profile input</param>
        /// <returns></returns>
        /// <response code="200">User profile successfully updated</response>
        /// <response code="400">Returns errors</response>
        [HttpPost]
        [Route("profile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateProfile([FromForm] UserProfileInput input)
        {
            var result = await UpdateProfileCommand.Value.ExecuteAsync(input);

            return PresentResult(result);
        }
    }
}
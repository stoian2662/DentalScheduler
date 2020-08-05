using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DentalScheduler.DTO.Input;
using DentalScheduler.Interfaces.UseCases;
using DentalScheduler.Web.RestService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DentalScheduler.Web.RestService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles=Roles.Admin)]
    public class RolesController : BaseApiController
    {
        public ICreateRoleCommand CreateRoleCommand { get; }

        public ILinkUserAndRoleCommand LinkUserAndRoleCommand { get; }

        public RolesController(
            ICreateRoleCommand createRoleCommand, 
            ILinkUserAndRoleCommand linkUserAndRoleCommand)
        {
            CreateRoleCommand = createRoleCommand;
            LinkUserAndRoleCommand = linkUserAndRoleCommand;
        }

        [HttpPost]
        [Route("/create")]
        public async Task<IActionResult> Create([FromBody] CreateRoleInput model)
        {
            var result = await CreateRoleCommand.CreateAsync(model);
            return PresentResult(result);
        }

        [HttpPost]
        [Route("/user/update")]
        public async Task<IActionResult> UpdateUser([FromBody] LinkUserAndRoleInput model)
        {
            var result = await LinkUserAndRoleCommand.ExecuteAsync(model);
            return PresentResult(result);
        }
    }
}
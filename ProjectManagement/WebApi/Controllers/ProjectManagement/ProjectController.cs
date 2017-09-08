using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.WebApi;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.Project.Commands;

namespace WebApi.Controllers.ProjectManagement
{
    [Route("api/project-management/")]
    public class ProjectController : BaseController
    {
        public ProjectController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] CreateProject command)
        {
            await commandQueryBus.SendAsync(command);
            return Created("api/project-management/", command.CreatedId);
        }

        [HttpPatch("assign-member")]
        public async Task<IActionResult> AssignMember([FromBody] AssignUserToProject command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }
    }
}

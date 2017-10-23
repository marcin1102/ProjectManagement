using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.WebApi;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Contracts.Project.Queries;

namespace WebApi.Controllers.ProjectManagement
{
    [Route("api/project-management/projects")]
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

        [HttpPatch("{projectId}/assign-member")]
        public async Task<IActionResult> AssignMember([FromRoute] Guid projectId, [FromBody] AssignUserToProject command)
        {
            command.ProjectId = projectId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpGet("{projectId}")]
        public Task<ProjectResponse> GetProject([FromRoute] Guid projectId)
        {
            return commandQueryBus.SendAsync(new GetProject(projectId));
        }

        [HttpGet]
        public Task<ICollection<ProjectResponse>> GetProjects()
        {
            return commandQueryBus.SendAsync(new GetProjects());
        }
    }
}

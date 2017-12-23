using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Message.CommandQueryBus;
using ProjectManagement.Infrastructure.WebApi;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Contracts.Label.Queries;
using ProjectManagement.Infrastructure.WebApi.Filters;
using ProjectManagementView.Contracts.Projects;

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

        [HttpGet]
        public Task<IReadOnlyCollection<ProjectListItem>> GetProjects([FromQuery] bool isAdmin)
        {
            if (isAdmin)
                return commandQueryBus.SendAsync(new GetProjectsAsAdmin());
            else
                return commandQueryBus.SendAsync(new GetProjects());
        }

        [HttpPost("{projectId}/labels")]
        public async Task<IActionResult> AddLabel([FromRoute] Guid projectId, [FromBody] AddLabel command)
        {
            command.ProjectId = projectId;
            await commandQueryBus.SendAsync(command);
            return Created("api/project-management/labels/", command.CreatedId);
        }

        [HttpGet("{projectId}/labels/{labelId}")]
        [ProducesResponseType(typeof(LabelResponse), 200)]
        [ProducesResponseType(typeof(ValidationFailureException), 400)]
        public Task<LabelResponse> GetLabel([FromRoute]Guid projectId, [FromRoute] Guid labelId)
        {
            return commandQueryBus.SendAsync(new GetLabel(labelId, projectId));
        }

        [HttpGet("{projectId}/labels")]
        [ProducesResponseType(typeof(ICollection<LabelResponse>), 200)]
        [ProducesResponseType(typeof(ValidationFailureException), 400)]
        public Task<ICollection<LabelResponse>> GetLabels([FromRoute] Guid projectId)
        {
            return commandQueryBus.SendAsync(new GetLabels(projectId));
        }

        [HttpGet("{projectId}/users")]
        [ProducesResponseType(typeof(IReadOnlyCollection<UserData>), 200)]
        public Task<IReadOnlyCollection<UserData>> GetUsers([FromRoute] Guid projectId)
        {
            return commandQueryBus.SendAsync(new GetUsers(projectId));
        }
    }
}

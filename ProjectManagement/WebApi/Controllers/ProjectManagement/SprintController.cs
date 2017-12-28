using ProjectManagement.Infrastructure.WebApi;
using System;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Message.CommandQueryBus;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.Sprint.Commands;
using ProjectManagementView.Contracts.Projects.Sprints;
using System.Collections.Generic;

namespace WebApi.Controllers.ProjectManagement
{
    [Route("api/project-management/projects/{projectId}/sprints/")]
    public class SprintController : BaseController
    {
        public SprintController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpPost]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Create([FromRoute] Guid projectId, [FromBody] CreateSprint command)
        {
            command.ProjectId = projectId;
            await commandQueryBus.SendAsync(command);
            return Created("api/project-management/sprint/", command.CreatedId);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<SprintListItem>), 200)]
        public Task<IReadOnlyCollection<SprintListItem>> Get([FromRoute] Guid projectId, [FromQuery] bool notFinishedOnly)
        {
            return commandQueryBus.SendAsync(new GetSprints(projectId, notFinishedOnly));
        }

        [HttpGet("{sprintId}")]
        [ProducesResponseType(typeof(SprintResponse), 200)]
        public Task<SprintResponse> Get([FromRoute] Guid projectId, [FromRoute] Guid sprintId)
        {
            return commandQueryBus.SendAsync(new GetSprint(projectId, sprintId));
        }

        [HttpPatch("{sprintId}/start")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> StartSprint([FromRoute] Guid projectId, [FromRoute] Guid sprintId)
        {
            await commandQueryBus.SendAsync(new StartSprint(sprintId, projectId));
            return Ok();
        }

        [HttpPatch("{sprintId}/finish")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> FinishSprint([FromRoute] Guid projectId, [FromRoute] Guid sprintId)
        {
            await commandQueryBus.SendAsync(new FinishSprint(sprintId, projectId));
            return Ok();
        }
    }
}

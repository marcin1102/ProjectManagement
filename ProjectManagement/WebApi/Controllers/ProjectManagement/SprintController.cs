using ProjectManagement.Infrastructure.WebApi;
using System;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Message.CommandQueryBus;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.Sprint.Commands;
using ProjectManagement.Contracts.Sprint.Queries;

namespace WebApi.Controllers.ProjectManagement
{
    [Route("api/project-management/projects/{projectId}/sprint/")]
    public class SprintController : BaseController
    {
        public SprintController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] Guid projectId, [FromBody] CreateSprint command)
        {
            command.ProjectId = projectId;
            await commandQueryBus.SendAsync(command);
            return Created("api/project-management/sprint/", command.CreatedId);
        }

        [HttpGet("{sprintId}")]
        public Task<SprintResponse> Get([FromRoute] Guid projectId, [FromRoute] Guid sprintId)
        {
            return commandQueryBus.SendAsync(new GetSprint(sprintId, projectId));
        }

        [HttpPatch("{sprintId}/start")]
        public async Task<IActionResult> StartSprint([FromRoute] Guid projectId, [FromRoute] Guid sprintId)
        {
            await commandQueryBus.SendAsync(new StartSprint(sprintId, projectId));
            return Ok();
        }

        [HttpPatch("{sprintId}/finish")]
        public async Task<IActionResult> FinishSprint([FromRoute] Guid projectId, [FromRoute] Guid sprintId)
        {
            await commandQueryBus.SendAsync(new FinishSprint(sprintId, projectId));
            return Ok();
        }
    }
}

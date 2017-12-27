using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.Bug.Commands;
using ProjectManagement.Infrastructure.Message.CommandQueryBus;
using ProjectManagement.Infrastructure.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Controllers.ProjectManagement.Issue
{
    [Route("api/project-management/projects/{projectId}/bugs/")]
    public class BugController : BaseController
    {
        public BugController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateBug([FromRoute] Guid projectId, [FromBody] CreateBug command)
        {
            command.ProjectId = projectId;
            await commandQueryBus.SendAsync(command);
            return Created("api/project-management/bugs/", command.CreatedId);
        }

        [HttpPatch("{bugId}/assign-labels")]
        public async Task<IActionResult> AssignLabels([FromRoute] Guid projectId, [FromRoute] Guid bugId, [FromBody] AssignLabelsToBug command)
        {
            command.ProjectId = projectId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{bugId}/comment")]
        public async Task<IActionResult> Comment([FromRoute] Guid projectId, [FromRoute] Guid bugId, [FromBody] CommentBug command)
        {
            command.ProjectId = projectId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{bugId}/mark-as-in-progress")]
        public async Task<IActionResult> MarkAsInProgress([FromRoute] Guid projectId, [FromRoute] Guid bugId, [FromBody] MarkBugAsInProgress command)
        {
            command.ProjectId = projectId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{bugId}/mark-as-done")]
        public async Task<IActionResult> MarkAsDone([FromRoute] Guid projectId, [FromRoute] Guid bugId, [FromBody] MarkBugAsDone command)
        {
            command.ProjectId = projectId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{bugId}/assign-assignee")]
        public async Task<IActionResult> AssignAssignee([FromRoute] Guid projectId, [FromRoute] Guid bugId, [FromBody] AssignAssigneeToBug command)
        {
            command.ProjectId = projectId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{bugId}/assign-to-sprint")]
        public async Task<IActionResult> AssignToSprint([FromRoute] Guid projectId, [FromRoute] Guid bugId, [FromBody] AssignBugToSprint command)
        {
            command.ProjectId = projectId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }
    }
}

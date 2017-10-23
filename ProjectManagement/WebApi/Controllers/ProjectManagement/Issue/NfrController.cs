using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.WebApi;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.Nfr.Commands;

namespace WebApi.Controllers.ProjectManagement.Issue
{
    [Route("api/project-management/projects/{projectId}/nfrs/")]
    public class NfrController : BaseController
    {
        public NfrController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateNfr([FromBody] CreateNfr command)
        {
            await commandQueryBus.SendAsync(command);
            return Created("api/project-management/nfrs/", command.CreatedId);
        }

        [HttpPatch("{nfrId}/assign-labels")]
        public async Task<IActionResult> AssignLabels([FromRoute] Guid projectId, [FromRoute] Guid nfrId, [FromBody] AssignLabelsToNfr command)
        {
            command.ProjectId = projectId;
            command.IssueId = nfrId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{nfrId}/comment")]
        public async Task<IActionResult> Comment([FromRoute] Guid projectId, [FromRoute] Guid nfrId, [FromBody] CommentNfr command)
        {
            command.ProjectId = projectId;
            command.IssueId = nfrId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{nfrId}/mark-as-in-progress")]
        public async Task<IActionResult> MarkAsInProgress([FromRoute] Guid projectId, [FromRoute] Guid nfrId, [FromBody] MarkNfrAsInProgress command)
        {
            command.ProjectId = projectId;
            command.IssueId = nfrId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{nfrId}/mark-as-done")]
        public async Task<IActionResult> MarkAsDone([FromRoute] Guid projectId, [FromRoute] Guid nfrId, [FromBody] MarkNfrAsDone command)
        {
            command.ProjectId = projectId;
            command.IssueId = nfrId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{nfrId}/assign-assignee")]
        public async Task<IActionResult> AssignAssignee([FromRoute] Guid projectId, [FromRoute] Guid nfrId, [FromBody] AssignAssigneeToNfr command)
        {
            command.ProjectId = projectId;
            command.IssueId = nfrId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{nfrId}/assign-to-sprint")]
        public async Task<IActionResult> AssignToSprint([FromRoute] Guid projectId, [FromRoute] Guid nfrId, [FromBody] AssignNfrToSprint command)
        {
            command.ProjectId = projectId;
            command.IssueId = nfrId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        #region Bugs
        [HttpPatch("{nfrId}/bugs/{bugId}/assign-labels")]
        public async Task<IActionResult> AssignLabelsToBug([FromRoute] Guid projectId, [FromRoute] Guid nfrId, [FromRoute] Guid bugId, [FromBody] AssignLabelsToNfrsBug command)
        {
            command.ProjectId = projectId;
            command.NfrId = nfrId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{nfrId}/bugs/{bugId}/comment")]
        public async Task<IActionResult> CommentBug([FromRoute] Guid projectId, [FromRoute] Guid nfrId, [FromRoute] Guid bugId, [FromBody] CommentNfrsBug command)
        {
            command.ProjectId = projectId;
            command.NfrId = nfrId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{nfrId}/bugs/{bugId}/mark-as-in-progress")]
        public async Task<IActionResult> MarkBugAsInProgress([FromRoute] Guid projectId, [FromRoute] Guid nfrId, [FromRoute] Guid bugId, [FromBody] MarkNfrsBugAsInProgress command)
        {
            command.ProjectId = projectId;
            command.NfrId = nfrId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{nfrId}/bugs/{bugId}/mark-as-done")]
        public async Task<IActionResult> MarkBugAsDone([FromRoute] Guid projectId, [FromRoute] Guid nfrId, [FromRoute] Guid bugId, [FromBody] MarkNfrsBugAsDone command)
        {
            command.ProjectId = projectId;
            command.NfrId = nfrId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{nfrId}/bugs/{bugId}/assign-assignee")]
        public async Task<IActionResult> AssignAssigneeToBug([FromRoute] Guid projectId, [FromRoute] Guid nfrId, [FromRoute] Guid bugId, [FromBody] AssignAssigneeToNfrsBug command)
        {
            command.ProjectId = projectId;
            command.NfrId = nfrId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{nfrId}/bugs/{bugId}/assign-to-sprint")]
        public async Task<IActionResult> AssignBugToSprint([FromRoute] Guid projectId, [FromRoute] Guid nfrId, [FromRoute] Guid bugId, [FromBody] AssignNfrsBugToSprint command)
        {
            command.ProjectId = projectId;
            command.NfrId = nfrId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{nfrId}/add-bug")]
        public async Task<IActionResult> AddToNfr([FromRoute] Guid projectId, [FromRoute] Guid nfrId, [FromBody] AddBugToNfr command)
        {
            command.ProjectId = projectId;
            command.NfrId = nfrId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }
        #endregion
    }
}

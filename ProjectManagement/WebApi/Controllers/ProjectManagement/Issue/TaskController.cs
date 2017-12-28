using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Message.CommandQueryBus;
using ProjectManagement.Infrastructure.WebApi;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.Task.Commands;
using ProjectManagementView.Contracts.Issues;

namespace WebApi.Controllers.ProjectManagement.Issue
{
    [Route("api/project-management/projects/{projectId}/tasks/")]
    public class TaskController : BaseController
    {
        public TaskController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromRoute] Guid projectId, [FromBody] CreateTask command)
        {
            command.ProjectId = projectId;
            await commandQueryBus.SendAsync(command);
            return Created("api/project-management/tasks/", command.CreatedId);
        }

        [HttpPatch("{taskId}/assign-labels")]
        public async Task<IActionResult> AssignLabels([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromBody] AssignLabelsToTask command)
        {
            command.ProjectId = projectId;
            command.IssueId = taskId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/comment")]
        public async Task<IActionResult> Comment([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromBody] CommentTask command)
        {
            command.ProjectId = projectId;
            command.IssueId = taskId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/mark-as-in-progress")]
        public async Task<IActionResult> MarkAsInProgress([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromBody] MarkTaskAsInProgress command)
        {
            command.ProjectId = projectId;
            command.IssueId = taskId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/mark-as-done")]
        public async Task<IActionResult> MarkAsDone([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromBody] MarkTaskAsDone command)
        {
            command.ProjectId = projectId;
            command.IssueId = taskId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/assign-assignee")]
        public async Task<IActionResult> AssignAssignee([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromBody] AssignAssigneeToTask command)
        {
            command.ProjectId = projectId;
            command.IssueId = taskId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/assign-to-sprint")]
        public async Task<IActionResult> AssignToSprint([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromBody] AssignTaskToSprint command)
        {
            command.ProjectId = projectId;
            command.IssueId = taskId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpGet("{taskId}/related-issues")]
        [ProducesResponseType(typeof(IReadOnlyCollection<IssueListItem>), 200)]
        public async Task<IReadOnlyCollection<IssueListItem>> GetRelatedIssues([FromRoute] Guid projectId, [FromRoute] Guid taskId)
        {
            var response = await commandQueryBus.SendAsync(new GetIssuesRelatedToTask(projectId, taskId));
            return response;
        }

        #region Bugs
        [HttpPatch("{taskId}/bugs/{bugId}/assign-labels")]
        public async Task<IActionResult> AssignLabelsToBug([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromRoute] Guid bugId, [FromBody] AssignLabelsToTasksBug command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/bugs/{bugId}/comment")]
        public async Task<IActionResult> CommentBug([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromRoute] Guid bugId, [FromBody] CommentTasksBug command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/bugs/{bugId}/mark-as-in-progress")]
        public async Task<IActionResult> MarkBugAsInProgress([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromRoute] Guid bugId, [FromBody] MarkTasksBugAsInProgress command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/bugs/{bugId}/mark-as-done")]
        public async Task<IActionResult> MarkBugAsDone([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromRoute] Guid bugId, [FromBody] MarkTasksBugAsDone command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/bugs/{bugId}/assign-assignee")]
        public async Task<IActionResult> AssignAssigneeToBug([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromRoute] Guid bugId, [FromBody] AssignAssigneeToTasksBug command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/bugs/{bugId}/assign-to-sprint")]
        public async Task<IActionResult> AssignBugToSprint([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromRoute] Guid bugId, [FromBody] AssignTasksBugToSprint command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            command.IssueId = bugId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPost("{taskId}/add-bug")]
        public async Task<IActionResult> AddToTask([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromBody] AddBugToTask command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            await commandQueryBus.SendAsync(command);
            return Created("", command.CreatedId);
        }
        #endregion

        #region Subtasks
        [HttpPatch("{taskId}/subtasks/{subtaskId}/assign-labels")]
        public async Task<IActionResult> AssignLabelsToSubtask([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromRoute] Guid subtaskId, [FromBody] AssignLabelsToSubtask command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            command.IssueId = subtaskId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/subtasks/{subtaskId}/comment")]
        public async Task<IActionResult> CommentSubtask([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromRoute] Guid subtaskId, [FromBody] CommentSubtask command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            command.IssueId = subtaskId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/subtasks/{subtaskId}/mark-as-in-progress")]
        public async Task<IActionResult> MarkSubtaskAsInProgress([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromRoute] Guid subtaskId, [FromBody] MarkSubtaskAsInProgress command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            command.IssueId = subtaskId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/subtasks/{subtaskId}/mark-as-done")]
        public async Task<IActionResult> MarkSubtaskAsDone([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromRoute] Guid subtaskId, [FromBody] MarkSubtaskAsDone command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            command.IssueId = subtaskId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/subtasks/{subtaskId}/assign-assignee")]
        public async Task<IActionResult> AssignAssigneeToSubtask([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromRoute] Guid subtaskId, [FromBody] AssignAssigneeToSubtask command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            command.IssueId = subtaskId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("{taskId}/subtasks/{subtaskId}/assign-to-sprint")]
        public async Task<IActionResult> AssignSubtaskToSprint([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromRoute] Guid subtaskId, [FromBody] AssignSubtaskToSprint command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            command.IssueId = subtaskId;
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPost("{taskId}/add-Subtask")]
        public async Task<IActionResult> AddToTask([FromRoute] Guid projectId, [FromRoute] Guid taskId, [FromBody] AddSubtaskToTask command)
        {
            command.ProjectId = projectId;
            command.TaskId = taskId;
            await commandQueryBus.SendAsync(command);
            return Created("", command.CreatedId);
        }
        #endregion
    }
}

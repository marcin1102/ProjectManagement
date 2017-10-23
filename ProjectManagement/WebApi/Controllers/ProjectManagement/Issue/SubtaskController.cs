using System.Threading.Tasks;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.WebApi;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.Subtask.Commands;

namespace WebApi.Controllers.ProjectManagement.Issue
{
    [Route("api/project-management/subtasks/")]
    public class SubtaskController : BaseController
    {
        public SubtaskController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubtask([FromBody] CreateSubtask command)
        {
            await commandQueryBus.SendAsync(command);
            return Created("api/project-management/subtasks/", command.CreatedId);
        }

        [HttpPatch("assign-labels")]
        public async Task<IActionResult> AssignLabels([FromBody] AssignLabelsToSubtask command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("comment")]
        public async Task<IActionResult> Comment([FromBody] CommentSubtask command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("mark-as-in-progress")]
        public async Task<IActionResult> MarkAsInProgress([FromBody] MarkSubtaskAsInProgress command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("mark-as-done")]
        public async Task<IActionResult> MarkAsDone([FromBody] MarkSubtaskAsDone command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("assign-assignee")]
        public async Task<IActionResult> AssignAssignee([FromBody] AssignAssigneeToSubtask command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("assign-to-sprint")]
        public async Task<IActionResult> AssignToSprint([FromBody] AssignSubtaskToSprint command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("add-to-task")]
        public async Task<IActionResult> AddToTask([FromBody] AddSubtaskToTask command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }
    }
}

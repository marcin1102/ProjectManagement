using System.Threading.Tasks;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.WebApi;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.Bug.Commands;

namespace WebApi.Controllers.ProjectManagement.Issue
{
    [Route("api/project-management/bugs/")]
    public class BugController : BaseController
    {
        public BugController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateBug([FromBody] CreateBug command)
        {
            await commandQueryBus.SendAsync(command);
            return Created("api/project-management/Bugs/", command.CreatedId);
        }

        [HttpPatch("assign-labels")]
        public async Task<IActionResult> AssignLabels([FromBody] AssignLabelsToBug command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("comment")]
        public async Task<IActionResult> Comment([FromBody] CommentBug command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("mark-as-in-progress")]
        public async Task<IActionResult> MarkAsInProgress([FromBody] MarkBugAsInProgress command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("mark-as-done")]
        public async Task<IActionResult> MarkAsDone([FromBody] MarkBugAsDone command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("assign-assignee")]
        public async Task<IActionResult> AssignAssignee([FromBody] AssignAssigneeToBug command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("assign-to-sprint")]
        public async Task<IActionResult> AssignToSprint([FromBody] AssignBugToSprint command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("add-to-task")]
        public async Task<IActionResult> AddToTask([FromBody] AddBugToTask command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("add-to-nfr")]
        public async Task<IActionResult> AddToNfr([FromBody] AddBugToNfr command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }
    }
}

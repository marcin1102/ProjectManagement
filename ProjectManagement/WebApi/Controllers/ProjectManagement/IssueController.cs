using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.WebApi;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.Issue.Commands;
using ProjectManagement.Contracts.Issue.Queries;

namespace WebApi.Controllers.ProjectManagement
{
    [Route("api/project-management/issues/")]
    public class IssueController : BaseController
    {
        public IssueController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateIssue([FromBody] CreateIssue command)
        {
            await commandQueryBus.SendAsync(command);
            return Created("api/project-management/issues/", command.CreatedId);
        }

        [HttpGet]
        public Task<IssueResponse> Get([FromQuery] GetIssue query)
        {
            return commandQueryBus.SendAsync(query);
        }

        [HttpGet("list")]
        public Task<ICollection<IssueListItem>> GetList([FromQuery] GetIssues query)
        {
            return commandQueryBus.SendAsync(query);
        }

        [HttpPatch("assign-labels")]
        public async Task<IActionResult> AssignLabels([FromBody] AssignLabelsToIssue command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("comment")]
        public async Task<IActionResult> Comment([FromBody] CommentIssue command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("add-subtask")]
        public async Task<IActionResult> AddSubtask([FromBody] AddSubtask command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("mark-as-in-progress")]
        public async Task<IActionResult> MarkAsInProgress([FromBody] MarkAsInProgress command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("mark-as-done")]
        public async Task<IActionResult> MarkAsDone([FromBody] MarkAsDone command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("assign-assignee")]
        public async Task<IActionResult> AssignAssignee([FromBody] AssignAssigneeToIssue command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }
    }
}

using Infrastructure.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Message.CommandQueryBus;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.Sprint.Commands;
using ProjectManagement.Contracts.Sprint.Queries;

namespace WebApi.Controllers.ProjectManagement
{
    [Route("api/project-management/sprint/")]
    public class SprintController : BaseController
    {
        public SprintController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSprint command)
        {
            await commandQueryBus.SendAsync(command);
            return Created("api/project-management/sprint/", command.CreatedId);
        }

        [HttpGet]
        public Task<SprintResponse> Get([FromQuery] GetSprint query)
        {
            return commandQueryBus.SendAsync(query);
        }

        [HttpPatch("start")]
        public async Task<IActionResult> StartSprint([FromBody] StartSprint command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }

        [HttpPatch("finish")]
        public async Task<IActionResult> FinishSprint([FromBody] FinishSprint command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok();
        }
    }
}

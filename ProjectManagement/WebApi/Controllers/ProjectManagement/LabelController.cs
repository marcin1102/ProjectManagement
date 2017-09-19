using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.WebApi;
using Infrastructure.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.Label.Commands;
using ProjectManagement.Contracts.Label.Queries;

namespace WebApi.Controllers.ProjectManagement
{
    [Route("api/project-management/labels/")]
    public class LabelController : BaseController
    {
        public LabelController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateLabel command)
        {
            await commandQueryBus.SendAsync(command);
            return Created("api/project-management/labels/", command.CreatedId);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(LabelResponse), 200)]
        [ProducesResponseType(typeof(ValidationFailureException), 400)]
        public Task<LabelResponse> GetLabel([FromRoute] Guid labelId)
        {
            return commandQueryBus.SendAsync(new GetLabel(labelId));
        }

        [HttpGet]
        [ProducesResponseType(typeof(LabelResponse), 200)]
        [ProducesResponseType(typeof(ValidationFailureException), 400)]
        public Task<ICollection<LabelResponse>> GetLabel([FromQuery] GetLabels query)
        {
            return commandQueryBus.SendAsync(query);
        }
    }
}

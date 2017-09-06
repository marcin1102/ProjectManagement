using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.WebApi;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Contracts.User.Commands;
using UserManagement.Contracts.User.Queries;

namespace WebApi.Controllers.UserManagement
{
    [Route("api/user-management/")]
    public class UserController : BaseController
    {
        public UserController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUser command)
        {
            await commandQueryBus.SendAsync(command);
            return Created("api/user-management/", command.CreatedId);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        public async Task<UserResponse> Get([FromRoute] Guid id)
        {
            var response = await commandQueryBus.SendAsync(new GetUser(id));
            return response;
        }

        [HttpPatch("{userId}")]
        public async Task<IActionResult> GrantRole([FromBody] GrantRole command)
        {
            await commandQueryBus.SendAsync(command);
            return NoContent();
        }
    }
}

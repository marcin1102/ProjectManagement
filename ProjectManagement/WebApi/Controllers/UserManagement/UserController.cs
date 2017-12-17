using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Message.CommandQueryBus;
using ProjectManagement.Infrastructure.WebApi;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Contracts.User.Commands;
using UserManagement.Contracts.User.Queries;

namespace WebApi.Controllers.UserManagement
{
    [Route("api/user-management/users")]
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

        [HttpGet("{email}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        public async Task<UserResponse> Get([FromRoute] string email)
        {
            var response = await commandQueryBus.SendAsync(new GetUser(email));
            return response;
        }

        [HttpPatch("{userId}/grant-role")]
        public async Task<IActionResult> GrantRole([FromRoute] Guid userId, [FromBody] GrantRole command)
        {
            command.UserId = userId;
            await commandQueryBus.SendAsync(command);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok($"token: {command.GeneratedToken}");
        }
    }
}

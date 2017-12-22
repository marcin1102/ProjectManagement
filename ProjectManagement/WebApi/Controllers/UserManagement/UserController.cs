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

        /// <summary>
        /// Allows admin to create new user with specific email and password
        /// </summary>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Create([FromBody] CreateUser command)
        {
            await commandQueryBus.SendAsync(command);
            return Created("api/user-management/", command.CreatedId);
        }

        /// <summary>
        /// Allows to get information about a user
        /// </summary>
        /// <param name="id">Id of the requested user</param>
        /// <returns>User profile</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(401)]
        public async Task<UserResponse> Get([FromRoute] Guid id)
        {
            var response = await commandQueryBus.SendAsync(new GetUser(id));
            return response;
        }
        /// <summary>
        /// Allows to get information about a user by his email
        /// </summary>
        /// <param name="email">Email of the requested user</param>
        /// <returns>User profile</returns>
        [HttpGet("{email}")]
        [ProducesResponseType(typeof(UserResponse), 200)]
        [ProducesResponseType(401)]
        public async Task<UserResponse> Get([FromRoute] string email)
        {
            var response = await commandQueryBus.SendAsync(new GetUser(email));
            return response;
        }

        /// <summary>
        /// Allows admin to grant user to a specific role
        /// </summary>
        /// <param name="userId"> an id of a user to grant role</param>
        /// <param name="command">An role and a version of the user</param>
        [HttpPatch("{userId}/grant-role")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GrantRole([FromRoute] Guid userId, [FromBody] GrantRole command)
        {
            command.UserId = userId;
            await commandQueryBus.SendAsync(command);
            return NoContent();
        }

        /// <summary>
        /// Allows person to login to the system
        /// </summary>
        /// <param name="command">Email and password</param>
        /// <returns>Authentication token</returns>
        [HttpPost("login")]
        [ProducesResponseType(typeof(String), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Login([FromBody] Login command)
        {
            await commandQueryBus.SendAsync(command);
            return Ok($"token: {command.GeneratedToken}");
        }

        /// <summary>
        /// Allows admin to get collection of users currently exist in the system
        /// </summary>
        /// <returns>Collection of basic users data</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<UserListItem>), 200)]
        [ProducesResponseType(401)]
        public async Task<IReadOnlyCollection<UserListItem>> Get()
        {
            var response = await commandQueryBus.SendAsync(new GetUsers());
            return response;
        }
    }
}

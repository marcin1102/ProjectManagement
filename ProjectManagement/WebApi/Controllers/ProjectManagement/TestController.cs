using System.Threading.Tasks;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.WebApi;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts;

namespace WebApi.Controllers.ProjectManagement
{
    [Route("api/project-management/")]
    public class TestController : BaseController
    {
        public TestController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpPost]
        [Route("test")]
        public async Task<IActionResult> TestEndpoint([FromBody] TestCommand command)
        {
            await commandQueryBus.SendAsync(command);
            return Created("api/project-management/test", command.TestValue);
        }

        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> TestEndpoint()
        {
            var response = await commandQueryBus.SendAsync(new TestQuery());
            return Ok(response);
        }
    }
}

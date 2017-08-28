using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> TestEndpoint([FromQuery] int testValue)
        {
            await commandQueryBus.SendAsync(new TestCommand(testValue));
            return Ok(testValue);
        }
    }
}

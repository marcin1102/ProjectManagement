using Infrastructure.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Message.CommandQueryBus;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Contracts.User.Queries;

namespace WebApi.Controllers.ProjectManagement
{
    [Route("api/project-management/users/")]
    public class MemberController : BaseController
    {
        public MemberController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [HttpGet("unfinished-issues/{sprintId}")]
        public Task<ICollection<UserListItem>> Get([FromRoute] Guid sprintId)
        {
            return commandQueryBus.SendAsync(new GetUnfinishedIssuesAssignees { SprintId = sprintId });
        }
    }
}

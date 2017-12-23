﻿using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Infrastructure.Message.CommandQueryBus;
using ProjectManagement.Infrastructure.WebApi;
using ProjectManagementView.Contracts.Issues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Controllers.ProjectManagement.Issue
{
    [Route("api/project-management/projects/{projectId}/issues/")]
    public class IssueController : BaseController
    {
        public IssueController(ICommandQueryBus commandQueryBus) : base(commandQueryBus)
        {
        }

        [ProducesResponseType(typeof(IReadOnlyCollection<IssueListItem>), 200)]
        public async Task<IReadOnlyCollection<IssueListItem>> GetIssues([FromRoute] Guid projectId)
        {
            var response = await commandQueryBus.SendAsync(new GetIssues(projectId));
            return response;
        }
    }
}
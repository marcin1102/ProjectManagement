using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Infrastructure.Storage.EF.Repository;
using ProjectManagementView.Contracts.Issues;
using ProjectManagementView.Helpers;
using ProjectManagementView.Storage.Models;
using ProjectManagementView.Storage.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Handlers.Issues
{
    public class TaskQueryHandler :
        IAsyncQueryHandler<GetIssuesRelatedToTask, IReadOnlyCollection<IssueListItem>>
    {
        private readonly IRepository<Storage.Models.Task> repository;

        public TaskQueryHandler(IRepository<Storage.Models.Task> repository)
        {
            this.repository = repository;
        }

        public async Task<IReadOnlyCollection<IssueListItem>> HandleAsync(GetIssuesRelatedToTask query)
        {
            var task = await repository.GetAsync(query.ParentIssueId);
            var bugs = task.Bugs.Cast<Issue>();
            var subtasks = task.Subtasks.Cast<Issue>();
            var issues = bugs.Concat(subtasks);
            return issues.Select(x => new IssueListItem(x.Id, x.ProjectId, IssueHelpers.GetIssueType(x), x.Title, x.Description, x.Status, x.Reporter.Id, x.Assignee?.Id)).ToList();
        }
    }
}

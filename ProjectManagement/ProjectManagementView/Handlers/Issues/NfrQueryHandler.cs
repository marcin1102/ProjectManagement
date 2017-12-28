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
    public class NfrQueryHandler :
        IAsyncQueryHandler<GetIssuesRelatedToNfr, IReadOnlyCollection<IssueListItem>>
    {
        private readonly IRepository<Storage.Models.Nfr> repository;

        public NfrQueryHandler(IRepository<Storage.Models.Nfr> repository)
        {
            this.repository = repository;
        }

        public async Task<IReadOnlyCollection<IssueListItem>> HandleAsync(GetIssuesRelatedToNfr query)
        {
            var task = await repository.GetAsync(query.ParentIssueId);
            var issues = task.Bugs.Cast<Issue>();
            return issues.Select(x => new IssueListItem(x.Id, x.ProjectId, IssueHelpers.GetIssueType(x), x.Title, x.Description, x.Status, x.Reporter.Id, x.Assignee?.Id)).ToList();
        }
    }
}

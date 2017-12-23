using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagementView.Contracts.Issues;
using ProjectManagementView.Contracts.Issues.Enums;
using ProjectManagementView.Searchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Handlers.Issues
{
    public class IssueQueryHandler :
        IAsyncQueryHandler<GetIssues, IReadOnlyCollection<IssueListItem>>
    {
        private readonly IIssueSearcher issueSearcher;

        public IssueQueryHandler(IIssueSearcher issueSearcher)
        {
            this.issueSearcher = issueSearcher;
        }

        public async Task<IReadOnlyCollection<IssueListItem>> HandleAsync(GetIssues query)
        {
            var issues = await issueSearcher.GetProjectIssues(query.ProjectId);
            return issues.Select(x => new IssueListItem(x.Id, x.ProjectId, GetIssueType(x), x.Title, x.Description, x.Status, x.Reporter.Id, x.Assignee?.Id)).ToList();
        }

        private IssueType GetIssueType(Storage.Models.Abstract.Issue issue)
        {
            if (typeof(Storage.Models.Task) == issue.GetType())
                return IssueType.Task;

            if (typeof(Storage.Models.Nfr) == issue.GetType())
                return IssueType.Nfr;

            if (typeof(Storage.Models.Bug) == issue.GetType())
                return IssueType.Bug;
                       
            return IssueType.Subtask;
        }
    }
}

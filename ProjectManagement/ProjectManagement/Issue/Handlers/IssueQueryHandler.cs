using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Issue.Queries;
using ProjectManagement.Issue.Repository;
using ProjectManagement.Issue.Searchers;
using Infrastructure.Exceptions;

namespace ProjectManagement.Issue.Handlers
{
    public class IssueQueryHandler :
        IAsyncQueryHandler<GetIssue, IssueResponse>,
        IAsyncQueryHandler<GetIssues, ICollection<IssueListItem>>
    {
        private readonly IssueRepository repository;
        private readonly IIssueSearcher searcher;

        public IssueQueryHandler(IssueRepository repository, IIssueSearcher searcher)
        {
            this.repository = repository;
            this.searcher = searcher;
        }

        public async Task<IssueResponse> HandleAsync(GetIssue query)
        {
            var issue = await repository.FindAsync(query.Id);
            if (issue == null)
                throw new EntityDoesNotExist(query.Id, nameof(Model.Issue));

            return new IssueResponse(issue.Id, issue.ProjectId, issue.SprintId, issue.Title, issue.Description, issue.Type, issue.Status,
                issue.Reporter.Id, issue.Assignee?.Id, issue.CreatedAt, issue.UpdatedAt, issue.Comments.OrderBy(x => x.CreatedAt).ToList(),
                issue.Subtasks.Select(x => x.SubtaskId).ToList(), issue.Labels.Select(x => x.LabelId).ToList());
        }

        public async Task<ICollection<IssueListItem>> HandleAsync(GetIssues query)
        {
            var issues = await searcher.GetIssues(query.ProjectId, query.ReporterId, query.AssigneeId);
            return issues.Select(x => new IssueListItem(x.Id, x.ProjectId, x.Title, x.Description, x.Type, x.Status, x.Reporter.Id, x.Assignee?.Id, x.CreatedAt, x.UpdatedAt)).ToList();
        }
    }
}

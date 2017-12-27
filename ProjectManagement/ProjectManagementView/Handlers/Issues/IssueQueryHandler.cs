using Newtonsoft.Json;
using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Infrastructure.Storage.EF.Repository;
using ProjectManagementView.Contracts.Issues;
using ProjectManagementView.Contracts.Issues.Enums;
using ProjectManagementView.Searchers;
using ProjectManagementView.Storage.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Handlers.Issues
{
    public class IssueQueryHandler :
        IAsyncQueryHandler<GetIssues, IReadOnlyCollection<IssueListItem>>,
        IAsyncQueryHandler<GetIssue, IssueResponse>
    {
        private readonly IIssueSearcher issueSearcher;
        private readonly IRepository<Issue> issueRepository;
        private readonly IUserSearcher userSearcher;

        public IssueQueryHandler(IIssueSearcher issueSearcher, IRepository<Issue> issueRepository, IUserSearcher userSearcher)
        {
            this.issueSearcher = issueSearcher;
            this.issueRepository = issueRepository;
            this.userSearcher = userSearcher;
        }

        public async Task<IReadOnlyCollection<IssueListItem>> HandleAsync(GetIssues query)
        {
            var issues = await issueSearcher.GetProjectIssues(query.ProjectId);
            return issues.Select(x => new IssueListItem(x.Id, x.ProjectId, GetIssueType(x), x.Title, x.Description, x.Status, x.Reporter.Id, x.Assignee?.Id)).ToList();
        }

        public async Task<IssueResponse> HandleAsync(GetIssue query)
        {
            var issue = await issueRepository.GetAsync(query.IssueId);
            var issueType = GetIssueType(issue);
            var usersNames = await userSearcher.GetUsers(issue.Comments.Select(x => x.MemberId).ToList());

            var issueResponse = new IssueResponse(issue.Id, issue.ProjectId, issue.SprintId, issueType, issue.Title, issue.Description,
                issue.Status, issue.Reporter.Id, issue.Reporter.GetFullName(), issue.Reporter.Email, issue.Assignee?.Id,
                issue.Comments.Select(x => new CommentResponse(x.MemberId, usersNames[x.MemberId], x.Content, x.AddedAt)).ToList(), 
                issue.Labels.Select(x => new LabelResponse(x.Id, x.Name, x.Description)).ToList(), issue.Version);

            var linkedIssues = new List<LinkedIssue>();
            if(issue is Storage.Models.Task)
            {
                var issues = await issueSearcher.GetIssuesRelatedToTask(issue.Id);
                linkedIssues = issues.Select(x => new LinkedIssue(x.Id, x.Title, GetIssueType(x))).ToList();
            }
            else if(issue is Storage.Models.Nfr)
            {
                var issues = await issueSearcher.GetIssuesRelatedToNfr(issue.Id);
                linkedIssues = issues.Select(x => new LinkedIssue(x.Id, x.Title, GetIssueType(x))).ToList();
            }
            else if(issue is Storage.Models.Subtask)
            {
                var parentIssue = await issueSearcher.GetParentIssueToSubtask(issue.Id);
                issueResponse.SetLinkedTo(parentIssue.Id, parentIssue.Title, IssueType.Task);
            }
            else
            {
                var parentIssue = await issueSearcher.GetParentIssueToBug(issue.Id);
                if(parentIssue != null)
                    issueResponse.SetLinkedTo(parentIssue.Id, parentIssue.Title, GetIssueType(parentIssue));
            }


            if (linkedIssues.Count != 0)
                issueResponse.SetLinkedIssues(linkedIssues);

            return issueResponse;
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

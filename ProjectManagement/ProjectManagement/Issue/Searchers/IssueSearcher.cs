using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Issue.Searchers
{
    public interface IIssueSearcher
    {
        Task<IReadOnlyCollection<Guid>> FindUnfinishedIssuesForSprint(Guid projectId, Guid sprintId);
    }

    public class IssueSearcher : IIssueSearcher
    {
        private readonly ProjectManagementContext db;

        public IssueSearcher(ProjectManagementContext db)
        {
            this.db = db;
        }

        public async Task<IReadOnlyCollection<Guid>> FindUnfinishedIssuesForSprint(Guid projectId, Guid sprintId)
        {
            var aggregateIssues = await db.AggregateIssue.Where(x => x.ProjectId == projectId && x.SprintId == sprintId).Where(x => x.Status != Contracts.Issue.Enums.IssueStatus.Done).ToListAsync();
            var issues = await db.Issues.Where(x => x.ProjectId == projectId && x.SprintId == sprintId).Where(x => x.Status != Contracts.Issue.Enums.IssueStatus.Done).ToListAsync();
            return aggregateIssues.Select(x => x.Id).Concat(issues.Select(x => x.Id)).ToList();
        }
    }
}

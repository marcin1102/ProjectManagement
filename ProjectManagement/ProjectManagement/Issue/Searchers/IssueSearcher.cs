using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Issue.Model;

namespace ProjectManagement.Issue.Searchers
{
    public interface IIssueSearcher
    {
        Task<List<Model.Issue>> GetIssues(Guid projectId, Guid? reporterId, Guid? assigneeId);
        Task<bool> DoesIssueExistInProject(Guid issueId, Guid projectId);
    }
    public class IssueSearcher : IIssueSearcher
    {
        private readonly ProjectManagementContext db;

        public IssueSearcher(ProjectManagementContext db)
        {
            this.db = db;
        }

        public Task<List<Model.Issue>> GetIssues(Guid projectId, Guid? reporterId, Guid? assigneeId)
        {
            return db.Issues.Include(x => x.Reporter).Include(x => x.Assignee)
                .Where(x => x.ProjectId == projectId)
                .Where(x => reporterId == null  || x.Reporter.Id == reporterId)
                .Where(x => assigneeId == null || x.Assignee.Id == assigneeId)
                .ToListAsync();
        }

        public Task<bool> DoesIssueExistInProject(Guid issueId, Guid projectId)
        {
            return db.Issues.AnyAsync(x => x.Id == issueId && x.ProjectId == projectId);
        }
    }
}

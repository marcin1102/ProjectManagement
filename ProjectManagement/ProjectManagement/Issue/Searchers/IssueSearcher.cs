using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Issue.Model;

namespace ProjectManagement.Issue.Searchers
{
    public interface IIssueSearcher
    {
        Task<List<Model.Issue>> GetIssues(Guid projectId, Guid? reporterId, Guid? assigneeId);
        Task<bool> DoesIssueExistInProject(Guid issueId, Guid projectId);
        Task<List<IssueStatus>> GetRelatedIssuesStatuses(Guid issueId);

        /// <summary>
        /// Check existence of issues in project scope
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="subtasksIds">Ids of issues to check</param>
        /// <returns>Ids of issues that do not exist in project scope</returns>
        Task<ICollection<Guid>> DoesIssuesExistInProject(Guid projectId, ICollection<Guid> subtasksIds);

        Task<Dictionary<Guid, Guid?>> GetUnfinishedIssues(Guid sprintId);
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

        public Task<List<IssueStatus>> GetRelatedIssuesStatuses(Guid issueId)
        {
            var query = from mainIssue in db.Issues
                        where mainIssue.Id == issueId
                        join issueSubtask in db.IssuesSubtasks on mainIssue.Id equals issueSubtask.IssueId
                        join subtask in db.Issues on issueSubtask.SubtaskId equals subtask.Id
                        select subtask.Status;
            return query.ToListAsync();
        }

        public async Task<ICollection<Guid>> DoesIssuesExistInProject(Guid projectId, ICollection<Guid> subtasksIds)
        {
            var response = new List<Guid>();
            var issuesIds = await db.Issues.Where(x => x.ProjectId == projectId).Where(x => subtasksIds.Contains(x.Id)).Select(x => x.Id).ToListAsync();
            return subtasksIds.Except(issuesIds).ToList();
        }

        public async Task<Dictionary<Guid, Guid?>> GetUnfinishedIssues(Guid sprintId)
        {
            var response = new Dictionary<Guid, Guid?>();
            var unfinishedIssues = await db.Issues.Where(x => x.SprintId.Value == sprintId && x.Status != IssueStatus.Done).Include(x => x.Assignee).ToListAsync();
            foreach (var issue in unfinishedIssues)
            {
                response.Add(issue.Id, issue.Assignee?.Id);
            }
            return response;
        }
    }
}

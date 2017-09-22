using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Sprint.Searchers
{
    public interface ISprintSearcher
    {
        Task<bool> DoesSprintExistInScope(Guid sprintId, Guid projectId);
    }

    public class SprintSearcher : ISprintSearcher
    {
        private readonly ProjectManagementContext db;

        public SprintSearcher(ProjectManagementContext db)
        {
            this.db = db;
        }

        public Task<bool> DoesSprintExistInScope(Guid sprintId, Guid projectId)
        {
            return db.Sprints.AnyAsync(x => x.Id == sprintId && x.ProjectId == projectId);
        }
    }
}

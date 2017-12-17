using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;

namespace ProjectManagement.Sprint.Searchers
{
    public interface ISprintSearcher
    {
        Task CheckIfSprintExistsInProjectScope(Guid sprintId, Guid projectId);
    }

    public class SprintSearcher : ISprintSearcher
    {
        private readonly ProjectManagementContext db;

        public SprintSearcher(ProjectManagementContext db)
        {
            this.db = db;
        }

        public async Task CheckIfSprintExistsInProjectScope(Guid sprintId, Guid projectId)
        {
            var doesExist = await db.Sprints.AnyAsync(x => x.Id == sprintId && x.ProjectId == projectId);
            if (!doesExist)
                throw new EntityDoesNotExistsInScope(sprintId, nameof(Sprint.Model.Sprint), nameof(Project.Model.Project), projectId);
        }
    }
}

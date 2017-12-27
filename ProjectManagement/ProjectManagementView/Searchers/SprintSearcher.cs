using Microsoft.EntityFrameworkCore;
using ProjectManagementView.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Searchers
{
    public interface ISprintSearcher
    {
        Task<List<Sprint>> GetSprints(Guid projectId, bool? notFinishedOnly = null);
    }

    public class SprintSearcher : ISprintSearcher
    {
        private readonly ProjectManagementViewContext db;

        public SprintSearcher(ProjectManagementViewContext db)
        {
            this.db = db;
        }

        public async Task<List<Sprint>> GetSprints(Guid projectId, bool? notFinishedOnly = null)
        {
            var project = await db.Projects
                .Include(x => x.Sprints)
                .SingleOrDefaultAsync(x => x.Id == projectId);
            return project.Sprints.Where(x => (notFinishedOnly == null || !notFinishedOnly.Value) ? true : x.Status != ProjectManagement.Contracts.Sprint.Enums.SprintStatus.Finished).ToList();
        }
    }
}

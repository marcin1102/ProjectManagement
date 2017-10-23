using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Label.Searcher
{
    public interface ILabelsSearcher
    {
        Task<List<Label>> GetLabels(Guid projectId, ICollection<Guid> labelsIds = null);
    }

    public class LabelSearcher : ILabelsSearcher
    {
        private readonly ProjectManagementContext db;

        public LabelSearcher(ProjectManagementContext db)
        {
            this.db = db;
        }

        public Task<List<Label>> GetLabels(Guid projectId, ICollection<Guid> labelsIds = null)
        {
            return db.Labels
                .Where(x => x.ProjectId == projectId)
                .Where(x => labelsIds == null ? true : labelsIds.Contains(x.Id))
                .ToListAsync();
        }
    }
}

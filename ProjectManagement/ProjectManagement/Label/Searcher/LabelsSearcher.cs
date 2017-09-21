using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.Label.Searcher
{
    public interface ILabelsSearcher
    {
        Task<List<Label>> GetLabels(Guid projectId);
        Task<ICollection<Guid>> DoesLabelsExistInScope(Guid projectId, ICollection<Guid> labelsIds);
    }

    public class LabelSearcher : ILabelsSearcher
    {
        private readonly ProjectManagementContext db;

        public LabelSearcher(ProjectManagementContext db)
        {
            this.db = db;
        }

        public Task<List<Label>> GetLabels(Guid projectId)
        {
            return db.Labels.Where(x => x.ProjectId == projectId).ToListAsync();
        }

        public async Task<ICollection<Guid>> DoesLabelsExistInScope(Guid projectId, ICollection<Guid> labelsIds)
        {
            var response = new List<Guid>();
            var ids = await db.Labels.Where(x => x.ProjectId == projectId).Where(x => labelsIds.Contains(x.Id)).Select(x => x.Id).ToListAsync();
            return labelsIds.Except(ids).ToList();
        }
    }
}

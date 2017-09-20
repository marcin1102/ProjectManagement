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
        Task<Dictionary<Guid, bool>> CheckIfLabelsExist(Guid projectId, ICollection<Guid> labelsIds);
    }

    public class LabelSearcher : ILabelsSearcher
    {
        private readonly ProjectManagementContext db;

        public LabelSearcher(ProjectManagementContext db)
        {
            this.db = db;
        }

        public async Task<Dictionary<Guid, bool>> CheckIfLabelsExist(Guid projectId, ICollection<Guid> labelsIds)
        {
            var response = new Dictionary<Guid, bool>();
            var labels = await db.Labels.Where(x => x.ProjectId == projectId).ToListAsync();
            foreach (var id in labelsIds)
            {
                response.Add(id, labels.Any(x => x.Id == id));
            }
            return response;
        }

        public Task<List<Label>> GetLabels(Guid projectId)
        {
            return db.Labels.Where(x => x.ProjectId == projectId).ToListAsync();
        }
    }
}

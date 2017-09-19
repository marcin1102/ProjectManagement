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
    }
}

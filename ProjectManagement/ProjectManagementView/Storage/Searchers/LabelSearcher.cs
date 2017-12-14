using Microsoft.EntityFrameworkCore;
using ProjectManagementView.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Storage.Searchers
{
    public interface ILabelSearcher
    {
        Task<List<Label>> GetLabels(List<Guid> ids);
    }

    public class LabelSearcher : ILabelSearcher
    {
        private readonly ProjectManagementViewContext context;

        public LabelSearcher(ProjectManagementViewContext context)
        {
            this.context = context;
        }

        public Task<List<Label>> GetLabels(List<Guid> ids)
        {
            return context.Labels.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}

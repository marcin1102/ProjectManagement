using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Message.EventDispatcher;
using ProjectManagement.Infrastructure.Storage.EF.Repository;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Issue.Model.Abstract;

namespace ProjectManagement.Issue.Repository
{
    public class SubtaskRepository : Repository<Model.Subtask>
    {
        public SubtaskRepository(ProjectManagementContext db) : base(db)
        {
            issueLabelQuery = db.Set<IssueLabel>();
            labelQuery = db.Set<Label.Label>();
        }

        private DbSet<IssueLabel> issueLabelQuery;
        private DbSet<Label.Label> labelQuery;

        public override Task AddAsync(Model.Subtask Subtask)
        {
            UpsertLabels(Subtask);
            return base.AddAsync(Subtask);
        }

        public override Task Update(Model.Subtask Subtask)
        {
            UpsertLabels(Subtask);
            return base.Update(Subtask);
        }

        private void UpsertLabels(Model.Subtask issue)
        {
            var updatedLabelsIds = issue.Labels.Select(x => x.Id).ToList();
            var currentlyAssignedLabels = issueLabelQuery.Where(x => x.IssueId == issue.Id);
            var currentlyAssignedLabelsIds = currentlyAssignedLabels.Select(x => x.LabelId).ToList();

            var toAdd = CreateInstancesOfIssueLabel(issue.Id, updatedLabelsIds.Except(currentlyAssignedLabelsIds));
            var toDelete = currentlyAssignedLabels.Where(x => !updatedLabelsIds.Any(y => x.LabelId == y)).ToList();

            issueLabelQuery.RemoveRange(toDelete);
            issueLabelQuery.AddRange(toAdd);
        }
        internal async Task FetchLabels(Model.Subtask Subtask)
        {
            var labelsIds = await issueLabelQuery.Where(x => x.IssueId == Subtask.Id).Select(x => x.LabelId).ToListAsync();
            var labels = await labelQuery.Where(x => labelsIds.Contains(x.Id)).ToListAsync();
            Subtask.Labels = labels;
        }

        protected ICollection<IssueLabel> CreateInstancesOfIssueLabel(Guid issueId, IEnumerable<Guid> labelsIds)
        {
            return labelsIds.Select(labelId => new IssueLabel(issueId, labelId)).ToList();
        }
    }
}

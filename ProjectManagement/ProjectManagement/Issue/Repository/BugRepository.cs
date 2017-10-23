using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message.EventDispatcher;
using Infrastructure.Storage.EF;
using Infrastructure.Storage.EF.Repository;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Issue.Model.Abstract;

namespace ProjectManagement.Issue.Repository
{
    public class BugRepository : Repository<Model.Bug>
    {
        public BugRepository(ProjectManagementContext db) : base(db)
        {
            issueLabelQuery = db.Set<IssueLabel>();
            labelQuery = db.Set<Label.Label>();
        }

        private DbSet<IssueLabel> issueLabelQuery;
        private DbSet<Label.Label> labelQuery;

        public override Task AddAsync(Model.Bug bug)
        {
            UpsertLabels(bug);
            return base.AddAsync(bug);
        }

        public override Task Update(Model.Bug bug)
        {
            UpsertLabels(bug);
            return base.Update(bug);
        }

        private void UpsertLabels(Model.Bug issue)
        {
            var updatedLabelsIds = issue.Labels.Select(x => x.Id).ToList();
            var currentlyAssignedLabels = issueLabelQuery.Where(x => x.IssueId == issue.Id);
            var currentlyAssignedLabelsIds = currentlyAssignedLabels.Select(x => x.LabelId).ToList();

            var toAdd = CreateInstancesOfIssueLabel(issue.Id, updatedLabelsIds.Except(currentlyAssignedLabelsIds));
            var toDelete = currentlyAssignedLabels.Where(x => !updatedLabelsIds.Any(y => x.LabelId == y)).ToList();

            issueLabelQuery.RemoveRange(toDelete);
            issueLabelQuery.AddRange(toAdd);
        }
        internal async Task FetchLabels(Model.Bug bug)
        {
            var labelsIds = await issueLabelQuery.Where(x => x.IssueId == bug.Id).Select(x => x.LabelId).ToListAsync();
            var labels = await labelQuery.Where(x => labelsIds.Contains(x.Id)).ToListAsync();
            bug.Labels = labels;
        }

        protected ICollection<IssueLabel> CreateInstancesOfIssueLabel(Guid issueId, IEnumerable<Guid> labelsIds)
        {
            return labelsIds.Select(labelId => new IssueLabel(issueId, labelId)).ToList();
        }
    }
}

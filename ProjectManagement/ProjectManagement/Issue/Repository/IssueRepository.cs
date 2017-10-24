using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.EventDispatcher;
using Infrastructure.Storage;
using Infrastructure.Storage.EF;
using Infrastructure.Storage.EF.Repository;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Issue.Model.Abstract;

namespace ProjectManagement.Issue.Repository
{
    public abstract class IssueRepository<TIssueAggregate> : AggregateRepository<TIssueAggregate>
        where TIssueAggregate : AggregateRoot, IIssue
    {
        public IssueRepository(ProjectManagementContext dbContext, IEventManager eventManager) : base(dbContext, eventManager)
        {
            issueLabelsQuery = dbContext.Set<IssueLabel>();
            labelsQuery = dbContext.Set<Label.Label>();
        }

        protected readonly DbSet<IssueLabel> issueLabelsQuery;
        protected readonly DbSet<Label.Label> labelsQuery;

        public override Task AddAsync(TIssueAggregate aggregate)
        {
            if (aggregate.Labels != null)
                UpsertLabels(aggregate);

            return base.AddAsync(aggregate);
        }

        public override Task Update(TIssueAggregate aggregate, long originalVersion)
        {
            if(aggregate.Labels != null)
                UpsertLabels(aggregate);

            return base.Update(aggregate, originalVersion);
        }

        private void UpsertLabels<TIssue>(TIssue issue)
            where TIssue : IEntity, IIssue
        {
            var updatedLabelsIds = issue.Labels.Select(x => x.Id).ToList();
            var currentlyAssignedLabels = issueLabelsQuery.Where(x => x.IssueId == issue.Id);
            var currentlyAssignedLabelsIds = currentlyAssignedLabels.Select(x => x.LabelId).ToList();

            var toAdd = CreateInstancesOfIssueLabel(issue.Id, updatedLabelsIds.Except(currentlyAssignedLabelsIds));
            var toDelete = currentlyAssignedLabels.Where(x => !updatedLabelsIds.Any(y => x.LabelId == y)).ToList();

            issueLabelsQuery.RemoveRange(toDelete);
            issueLabelsQuery.AddRange(toAdd);
        }

        protected virtual ICollection<IssueLabel> CreateInstancesOfIssueLabel(Guid issueId, IEnumerable<Guid> labelsIds)
        {
            return labelsIds.Select(labelId => new IssueLabel(issueId, labelId)).ToList();
        }

        public override async Task<TIssueAggregate> FindAsync(Guid id)
        {
            var issueAggregate = await Query.Include(x => x.Comments).SingleOrDefaultAsync(x => x.Id == id);

            if (issueAggregate == null)
                return null;

            await FetchLabels(issueAggregate);
            return issueAggregate;
        }

        public override async Task<TIssueAggregate> GetAsync(Guid id)
        {
            var issueAggregate = await Query.Include(x => x.Comments).SingleOrDefaultAsync(x => x.Id == id) ??
                throw new EntityDoesNotExist(id, typeof(TIssueAggregate).Name);

            await FetchLabels(issueAggregate);
            return issueAggregate;
        }

        internal async Task FetchLabels(TIssueAggregate issueAggregate)
        {
            var labelsIds = await issueLabelsQuery.Where(x => x.IssueId == issueAggregate.Id).Select(x => x.LabelId).ToListAsync();
            var labels = await labelsQuery.Where(x => labelsIds.Contains(x.Id)).ToListAsync();
            issueAggregate.Labels = labels;
        }

        public async Task UpdateChildEntity<TChildEntity>(TIssueAggregate issue, long aggregateOriginalVersion, TChildEntity childEntity)
            where TChildEntity : class, IEntity, IIssue
        {
            var childEntityQuery = dbContext.Set<TChildEntity>();

            if(childEntity.Labels != null)
                UpsertLabels(childEntity);

            var entry = dbContext.Entry(childEntity);
            if (entry.State == EntityState.Detached)
            {
                childEntityQuery.Add(childEntity);
            }

            var aggregateEntry = dbContext.Entry(issue);
            if (entry.State == EntityState.Detached)
            {
                Query.Add(issue);
            }
            else
            {
                if (aggregateOriginalVersion != aggregateEntry.Property(x => x.Version).OriginalValue)
                    throw new ConcurrentModificationException(issue.Id, typeof(TIssueAggregate).Name);
            }

            await dbContext.SaveChangesAsync();
            await eventManager.PublishEventsAsync(issue.PendingEvents);
        }
    }
}

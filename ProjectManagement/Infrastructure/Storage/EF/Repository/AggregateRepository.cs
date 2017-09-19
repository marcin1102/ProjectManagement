using System;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.EventDispatcher;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Storage.EF.Repository
{
    public class AggregateRepository<TAggregate>
        where TAggregate : class, IAggregateRoot
    {
        public readonly DbContext dbContext;
        private readonly IEventManager eventManager;

        private readonly DbSet<TAggregate> dbSet;
        public DbSet<TAggregate> Query => dbSet;

        public AggregateRepository(DbContext dbContext, IEventManager eventManager)
        {
            this.dbContext = dbContext;
            this.eventManager = eventManager;
            dbSet = dbContext.Set<TAggregate>();
        }

        public virtual async Task AddAsync(TAggregate aggregate, long originalVersion)
        {
            await AddOrUpdate(aggregate, originalVersion);
            await eventManager.PublishEventsAsync(aggregate.PendingEvents);
        }

        public async Task Update(TAggregate aggregate, long originalVersion)
        {
            await AddOrUpdate(aggregate, originalVersion);
            await eventManager.PublishEventsAsync(aggregate.PendingEvents);
        }

        public virtual Task<TAggregate> GetAsync(Guid id)
        {
            return Query.SingleOrDefaultAsync(x => x.Id == id) ?? throw new EntityDoesNotExist(id, typeof(TAggregate).Name);
        }

        public virtual Task<TAggregate> FindAsync(Guid id)
        {
            return Query.SingleOrDefaultAsync(x => x.Id == id);
        }

        private async Task AddOrUpdate(TAggregate aggregate, long version)
        {
            var entry = dbContext.Entry(aggregate);
            if(entry.State == EntityState.Detached)
            {
                Query.Add(aggregate);
            }
            else
            {
                if(version != entry.Property(x => x.Version).OriginalValue)
                    throw new ConcurrentModificationException(aggregate.Id, typeof(TAggregate).Name);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}

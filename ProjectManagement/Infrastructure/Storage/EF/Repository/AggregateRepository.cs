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
        private readonly DbContext dbContext;
        private readonly IEventManager eventManager;

        private readonly DbSet<TAggregate> dbSet;
        public DbSet<TAggregate> Query => dbSet;

        public AggregateRepository(DbContext dbContext, IEventManager eventManager)
        {
            this.dbContext = dbContext;
            this.eventManager = eventManager;
            dbSet = dbContext.Set<TAggregate>();
        }

        public virtual async Task AddAsync(TAggregate aggregate)
        {
            await Query.AddAsync(aggregate);
            await dbContext.SaveChangesAsync();
            await eventManager.PublishEventsAsync(aggregate.PendingEvents);
        }

        public async Task Update(TAggregate aggregate, long version)
        {
            if (version != aggregate.Version - 1)
                throw new ConcurrentModificationException(aggregate.Id, typeof(TAggregate).Name);

            dbContext.Entry(aggregate).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
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
    }
}

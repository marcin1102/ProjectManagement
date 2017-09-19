using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Storage.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Message.EventDispatcher
{
    public interface IEventManager
    {
        Task PublishEventsAsync<TEvent>(Queue<TEvent> events)
            where TEvent : IDomainEvent;
    }

    public class EventManager : IEventManager
    {
        private readonly IDomainEventDispatcher eventDispatcher;
        private readonly BaseDbContext dbContext;
        private readonly ILogger logger;
        private readonly DbSet<EventEnvelope> Query;

        public EventManager(IDomainEventDispatcher eventDispatcher, BaseDbContext dbContext, ILoggerFactory logger)
        {
            this.eventDispatcher = eventDispatcher;
            this.dbContext = dbContext;
            Query = dbContext.Set<EventEnvelope>();
            this.logger = logger.CreateLogger(nameof(EventManager));
        }

        public async Task PublishEventsAsync<TEvent>(Queue<TEvent> eventsToSend)
            where TEvent : IDomainEvent
        {
            await SaveEvents(eventsToSend);

            foreach (var @event in eventsToSend)
            {
                try
                {
                    await eventDispatcher.Dispatch(@event);
                }
                catch (DbUpdateException ex)
                {
                    logger.LogError(ex.Source, ex.Message);
                    throw ex;
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Source, ex.Message);
                    throw ex;
                }
            }
        }

        private async Task SaveEvents<TEvent>(Queue<TEvent> eventsToSend) where TEvent : IDomainEvent
        {
            var eventsToSave = eventsToSend.Select(x => new EventEnvelope(x));
            await Query.AddRangeAsync(eventsToSave);
            await dbContext.SaveChangesAsync();
        }
    }
}

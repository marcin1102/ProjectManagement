using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Message.Handlers;

namespace Infrastructure.Message.EventDispatcher
{
    public interface IEventBroker
    {
        Task DeliverEventTo<TEvent>(TEvent @event, Assembly assemblyToDeliverEvent)
            where TEvent : IDomainEvent;
    }

    public class EventBroker : IEventBroker
    {
        private readonly Type eventHandlerType = typeof(IAsyncEventHandler<>);
        private readonly IComponentContext context;

        public EventBroker(IComponentContext context)
        {
            this.context = context;
        }

        public async Task DeliverEventTo<TEvent>(TEvent @event, Assembly assemblyToDeliverEvent)
            where TEvent : IDomainEvent
        {
            var domainEventType = @event.GetType();
            var genericEventHandlerType = eventHandlerType.MakeGenericType(domainEventType);
            var subscribersTypes = assemblyToDeliverEvent.GetTypes().Where(type => type.GetInterfaces().Contains(genericEventHandlerType));
            var subscribers = subscribersTypes
                .Select(x => context.Resolve(x))
                .Select(x => (IAsyncEventHandler<TEvent>)x)
                .ToList();

            foreach (var subscriber in subscribers)
            {
                await subscriber.HandleAsync(@event);
            }
        }
    }
}

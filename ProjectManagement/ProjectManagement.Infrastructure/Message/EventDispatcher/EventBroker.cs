using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Infrastructure.Primitives.Message;

namespace ProjectManagement.Infrastructure.Message.EventDispatcher
{
    public interface IEventBroker
    {
        Task DeliverEventTo<TEvent>(TEvent @event, Assembly assemblyToDeliverEvent)
            where TEvent : IDomainEvent;
    }

    public class EventBroker : IEventBroker
    {
        private readonly Type eventHandlerType = typeof(IAsyncEventHandler<>);
        private readonly Type eventHandlerWrapperType = typeof(EventHandlerWrapper<>);
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
            var wrapperGenericType = eventHandlerWrapperType.MakeGenericType(domainEventType);

            var subscribers = subscribersTypes
                .Select(x => context.Resolve(x))
                .ToList();

            var wrappers = subscribers.Select(x => (EventHandlerWrapper)Activator.CreateInstance(wrapperGenericType, x));

            foreach (var wrapper in wrappers)
            {
                await wrapper.HandleAsync(@event);
            }
        }

        private abstract class EventHandlerWrapper
        {
            public abstract Task HandleAsync(IDomainEvent @event);
        }

        private class EventHandlerWrapper<TEvent> : EventHandlerWrapper
            where TEvent : IDomainEvent
        {
            private readonly IAsyncEventHandler<TEvent> handler;

            public EventHandlerWrapper(IAsyncEventHandler<TEvent> handler)
            {
                this.handler = handler;
            }

            public override Task HandleAsync(IDomainEvent @event)
            {
                var castedEvent = (TEvent)@event;
                return handler.HandleAsync(castedEvent);
            }
        }
    }
}

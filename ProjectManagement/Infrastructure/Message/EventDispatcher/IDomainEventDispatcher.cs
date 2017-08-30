using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Message.EventDispatcher
{
    public interface IDomainEventDispatcher
    {
        Task Dispatch<TEvent>(TEvent @event)
            where TEvent : IDomainEvent;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;
using Infrastructure.Storage.EF;

namespace Infrastructure.Storage
{
    public interface IAggregateRoot : IEntity
    {
        Queue<IDomainEvent> PendingEvents { get; }
        long Version { get; }
    }
}

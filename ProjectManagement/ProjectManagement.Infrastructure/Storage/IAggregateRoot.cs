using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Infrastructure.Message;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagement.Infrastructure.Storage.EF;

namespace ProjectManagement.Infrastructure.Storage
{
    public interface IAggregateRoot : IEntity
    {
        Queue<IDomainEvent> PendingEvents { get; }
        long Version { get; }
    }
}

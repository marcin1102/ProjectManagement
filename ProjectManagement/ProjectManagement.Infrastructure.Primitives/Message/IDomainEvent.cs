using System;

namespace ProjectManagement.Infrastructure.Primitives.Message
{
    public interface IDomainEvent
    {
        long AggregateVersion { get; set; }
    }
}

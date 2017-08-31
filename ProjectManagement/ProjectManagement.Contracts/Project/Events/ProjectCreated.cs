using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Project.Events
{
    public class ProjectCreated : IDomainEvent
    {
        public ProjectCreated(Guid id, string name, long aggregateVersion)
        {
            Id = id;
            Name = name;
            AggregateVersion = aggregateVersion;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public long AggregateVersion { get; private set; }
    }
}

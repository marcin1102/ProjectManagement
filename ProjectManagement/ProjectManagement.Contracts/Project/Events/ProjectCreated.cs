using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Project.Events
{
    public class ProjectCreated : IDomainEvent
    {
        public ProjectCreated(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public long AggregateVersion { get; set; }
    }
}

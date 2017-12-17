using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Message;

namespace ProjectManagement.Contracts.Project.Events
{
    public class LabelAdded : IDomainEvent
    {
        public LabelAdded(Guid id, Guid projectId, string name, string description)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
            Description = description;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public long AggregateVersion { get; set; }
    }
}

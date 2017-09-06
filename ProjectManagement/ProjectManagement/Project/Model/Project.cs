using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage;
using ProjectManagement.Contracts.Project.Events;

namespace ProjectManagement.Project.Model
{
    public class Project : AggregateRoot
    {
        public Project(Guid id, string name) : base(id)
        {
            Name = name;
            Version = 0;
            Update(new ProjectCreated(Id, Name));
        }

        public string Name { get; private set; }
        //ICollection<Member>
        public long Version { get; private set; }
    }
}

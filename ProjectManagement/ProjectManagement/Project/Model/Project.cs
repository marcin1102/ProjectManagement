using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage;

namespace ProjectManagement.Project.Model
{
    public class Project : IAggregateRoot
    {
        public Project() { }
        public Project(Guid id, string name)
        {
            Id = id;
            Name = name;
            Version = 0;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        //ICollection<Member>
        public long Version { get; private set; }
    }
}

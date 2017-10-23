using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;

namespace ProjectManagementView.Storage.Models
{
    public class Project : IEntity
    {
        public Project(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        public string Name { get; set; }
        public ICollection<Sprint> Sprints { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Nfr> Nfrs { get; set; }
        public ICollection<Bug> Bugs { get; set; }
        public ICollection<Subtask> Subtasks { get; set; }
        public ICollection<Label> Labels { get; set; }

        public long Version { get; set; }
    }
}

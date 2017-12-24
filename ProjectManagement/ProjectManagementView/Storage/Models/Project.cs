using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Infrastructure.Storage.EF;

namespace ProjectManagementView.Storage.Models
{
    public class Project : IEntity
    {
        private Project() { }

        public Project(Guid id)
        {
            Id = id;
            Sprints = new List<Sprint>();
            Users = new List<User>();
            Tasks = new List<Task>();
            Nfrs = new List<Nfr>();
            Bugs = new List<Bug>();
            Subtasks = new List<Subtask>();
            Labels = new List<Label>();
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

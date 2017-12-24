using System;
using System.Collections.Generic;
using ProjectManagement.Infrastructure.Storage.EF;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Sprint.Enums;

namespace ProjectManagementView.Storage.Models
{
    public class Sprint : IEntity
    {
        private Sprint()
        {

        }

        public Sprint(Guid id)
        {
            Id = id;
            Tasks = new List<Task>();
            Nfrs = new List<Nfr>();
            Bugs = new List<Bug>();
            Subtasks = new List<Subtask>();
        }

        public Guid Id { get; private set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public SprintStatus Status { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Nfr> Nfrs { get; set; }
        public ICollection<Bug> Bugs { get; set; }
        public ICollection<Subtask> Subtasks { get; set; }
        public string unfinishedIssues { get; private set; }
        public ICollection<UnfinishedIssue> UnfinishedIssues
        {
            get => JsonConvert.DeserializeObject<ICollection<UnfinishedIssue>>(unfinishedIssues);
            set
            {
                unfinishedIssues = JsonConvert.SerializeObject(value);
            }
        }

        public long Version { get; set; }
    }
}

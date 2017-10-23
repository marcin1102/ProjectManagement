using System;
using System.Collections.Generic;
using Infrastructure.Storage.EF;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Sprint.Enums;

namespace ProjectManagementView.Storage.Models
{
    public class Sprint : IEntity
    {
        public Sprint(Guid id)
        {
            Id = id;
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

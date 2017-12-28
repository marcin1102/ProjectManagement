using System;
using System.Collections.Generic;
using ProjectManagement.Infrastructure.Storage.EF;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Sprint.Enums;
using ProjectManagementView.Contracts.Issues;

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
            UnfinishedIssues = new List<UnfinishedIssue>();
        }

        public Guid Id { get; private set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public SprintStatus Status { get; set; }
        public List<Task> Tasks { get; set; }
        public List<Nfr> Nfrs { get; set; }
        public List<Bug> Bugs { get; set; }
        public List<Subtask> Subtasks { get; set; }
        public string unfinishedIssues { get; private set; }
        public List<UnfinishedIssue> UnfinishedIssues
        {
            get => JsonConvert.DeserializeObject<List<UnfinishedIssue>>(unfinishedIssues);
            set
            {
                unfinishedIssues = JsonConvert.SerializeObject(value);
            }
        }

        public long Version { get; set; }
    }
}

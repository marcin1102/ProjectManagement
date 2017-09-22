using Infrastructure.Storage;
using Newtonsoft.Json;
using ProjectManagement.Contracts.DomainExceptions;
using ProjectManagement.Contracts.Sprint.Enums;
using ProjectManagement.Contracts.Sprint.Events;
using ProjectManagement.Contracts.Sprint.ValueObjects;
using ProjectManagement.Providers;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Sprint.Model
{
    public class Sprint : AggregateRoot
    {
        public Guid ProjectId { get; private set; }
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public SprintStatus Status { get; private set; }

        public string unfinishedIssues { get; private set; }
        public ICollection<UnfinishedIssue> UnfinishedIssues
        {
            get => JsonConvert.DeserializeObject<ICollection<UnfinishedIssue>>(unfinishedIssues ?? "");
            private set
            {
                unfinishedIssues = JsonConvert.SerializeObject(value);
            }
        }

        private Sprint() { }
        public Sprint(Guid id, Guid projectId, string name, DateTime startDate, DateTime endDate) : base(id)
        {
            ProjectId = projectId;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Status = SprintStatus.Planned;
        }

        public override void Created()
        {
            Update(new SprintCreated(Id, Name, StartDate.Date, EndDate.Date));
        }

        public void StartSprint()
        {
            if (Status != SprintStatus.Planned)
                throw new CannotChangeSprintStatus(Id, Status, SprintStatus.InProgress, DomainInformationProvider.Name);

            var currentDate = DateTime.Now.Date;
            if (StartDate.Date != currentDate)
                StartDate = currentDate;

            Status = SprintStatus.InProgress;
            Update(new SprintStarted(Id, Status));
        }

        public void FinishSprint(Dictionary<Guid, Guid?> unfinishedIssuesUsers)
        {
            if (Status != SprintStatus.InProgress)
                throw new CannotChangeSprintStatus(Id, Status, SprintStatus.Finished, DomainInformationProvider.Name);

            EnsureUnfinishedIssuesSaved(unfinishedIssuesUsers);
            var currentDate = DateTime.Now.Date;
            if (EndDate.Date != currentDate)
                EndDate = currentDate;

            Status = SprintStatus.Finished;
            Update(new SprintFinished(Id, Status, UnfinishedIssues));
        }

        private void EnsureUnfinishedIssuesSaved(Dictionary<Guid, Guid?> unfinishedIssuesUsers)
        {
            var unfinishedIssues = new List<UnfinishedIssue>();
            foreach (var issueUser in unfinishedIssuesUsers)
            {
                unfinishedIssues.Add(new UnfinishedIssue(issueUser.Key, issueUser.Value));
            }
            UnfinishedIssues = unfinishedIssues;
        }
    }
}

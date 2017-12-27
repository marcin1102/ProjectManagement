using ProjectManagement.Infrastructure.Storage;
using Newtonsoft.Json;
using ProjectManagement.Contracts.DomainExceptions;
using ProjectManagement.Contracts.Sprint.Enums;
using ProjectManagement.Contracts.Sprint.Events;
using ProjectManagement.Contracts.Sprint.ValueObjects;
using ProjectManagement.Providers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Sprint.Searchers;
using System.Linq;
using ProjectManagement.Contracts.Sprint.Exceptions;

namespace ProjectManagement.Sprint.Model
{
    public class Sprint : AggregateRoot
    {
        public Guid ProjectId { get; private set; }
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public SprintStatus Status { get; private set; }

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
            Update(new SprintCreated(Id, ProjectId, Name, StartDate.Date, EndDate.Date, Status));
        }

        public async Task StartSprint(ISprintSearcher sprintSearcher)
        {
            if (Status != SprintStatus.Planned)
                throw new CannotChangeSprintStatus(Id, Status, SprintStatus.InProgress, DomainInformationProvider.Name);

            var sprints = await sprintSearcher.GetSprints(ProjectId);
            if (sprints.Any(x => x.Status == SprintStatus.InProgress))
                throw new CannotStartSprintWhenAnyOtherIsNotFinished(Id);

            var currentDate = DateTime.UtcNow.Date;
            if (StartDate.Date != currentDate)
                StartDate = currentDate;

            Status = SprintStatus.InProgress;
            Update(new SprintStarted(Id, Status, StartDate));
        }

        public void FinishSprint()
        {
            if (Status != SprintStatus.InProgress)
                throw new CannotChangeSprintStatus(Id, Status, SprintStatus.Finished, DomainInformationProvider.Name);

            var currentDate = DateTime.UtcNow.Date;
            if (EndDate.Date != currentDate)
                EndDate = currentDate;

            Status = SprintStatus.Finished;
            Update(new SprintFinished(Id, Status, EndDate));
        }
    }
}

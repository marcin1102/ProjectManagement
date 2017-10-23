using Infrastructure.Message;
using ProjectManagement.Contracts.Sprint.Enums;
using ProjectManagement.Contracts.Sprint.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Contracts.Sprint.Queries
{
    public class GetSprint : IQuery<SprintResponse>
    {
        public GetSprint(Guid id, Guid projectId)
        {
            Id = id;
            ProjectId = projectId;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
    }

    public class SprintResponse
    {
        public SprintResponse(Guid id, Guid projectId, string name, DateTime startDate, DateTime endDate, SprintStatus status)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Name { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public SprintStatus Status { get; private set; }
    }
}

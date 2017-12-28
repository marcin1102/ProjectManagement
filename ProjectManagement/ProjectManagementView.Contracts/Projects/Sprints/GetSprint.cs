using Newtonsoft.Json;
using ProjectManagement.Contracts.Sprint.Enums;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagementView.Contracts.Issues;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Contracts.Projects.Sprints
{
    public class GetSprint : IQuery<SprintResponse>
    {
        public GetSprint(Guid projectId, Guid sprintId)
        {
            ProjectId = projectId;
            SprintId = sprintId;
        }

        [JsonIgnore]
        public Guid ProjectId { get; private set; }
        [JsonIgnore]
        public Guid SprintId { get; private set; }
    }

    public class SprintResponse
    {
        public SprintResponse(Guid id, string name, DateTime start, DateTime end, SprintStatus status, IReadOnlyCollection<UnfinishedIssue> unfinishedIssues, long version)
        {
            Id = id;
            Name = name;
            Start = start;
            End = end;
            Status = status;
            UnfinishedIssues = unfinishedIssues;
            Version = version;
        }

        public Guid Id { get; }
        public string Name { get; }
        public DateTime Start { get; }
        public DateTime End { get; }
        public SprintStatus Status { get; }
        public IReadOnlyCollection<UnfinishedIssue> UnfinishedIssues { get; }
        public long Version { get; }
    }
}

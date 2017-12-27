using ProjectManagement.Contracts.Sprint.Enums;
using ProjectManagement.Infrastructure.Primitives.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Contracts.Projects.Sprints
{
    public class GetSprints : IQuery<IReadOnlyCollection<SprintListItem>>
    {
        public GetSprints(Guid projectId, bool notFinishedOnly)
        {
            ProjectId = projectId;
            NotFinishedOnly = notFinishedOnly;
        }

        public Guid ProjectId { get; private set; }
        public bool NotFinishedOnly { get; private set; }
    }

    public class SprintListItem
    {
        public SprintListItem(Guid id, string name, DateTime start, DateTime end, SprintStatus status)
        {
            Id = id;
            Name = name;
            Start = start;
            End = end;
            Status = status;
        }

        public Guid Id { get; }
        public string Name { get; }
        public DateTime Start { get; }
        public DateTime End { get; }
        public SprintStatus Status{ get; }
        public string NameWithDate => $"{Name}({Start.Date} : {End.Date})";
    }
}

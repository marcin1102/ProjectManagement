using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Task.Commands
{
    public class AssignAssigneeToSubtask : AssignAssigneeToIssue
    {
        public AssignAssigneeToSubtask(Guid assigneeId) : base(assigneeId)
        {
        }

        [JsonIgnore]
        public Guid TaskId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Task.Commands
{
    public class AssignLabelsToTasksBug : AssignLabelsToIssue
    {
        public AssignLabelsToTasksBug(ICollection<Guid> labelsIds) : base(labelsIds)
        {
        }

        [JsonIgnore]
        public Guid TaskId { get; set; }
    }
}

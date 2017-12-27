using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Nfr.Commands
{
    public class AssignAssigneeToNfrsBug : AssignAssigneeToIssue
    {
        public AssignAssigneeToNfrsBug(Guid assigneeId) : base(assigneeId)
        {
        }

        [JsonIgnore]
        public Guid NfrId { get; set; }
    }
}

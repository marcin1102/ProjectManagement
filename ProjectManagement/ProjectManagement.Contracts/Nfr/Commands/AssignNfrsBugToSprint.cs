using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Nfr.Commands
{
    public class AssignNfrsBugToSprint : AssignIssueToSprint
    {
        public AssignNfrsBugToSprint(Guid sprintId) : base(sprintId)
        {
        }

        [JsonIgnore]
        public Guid NfrId { get; set; }
    }
}

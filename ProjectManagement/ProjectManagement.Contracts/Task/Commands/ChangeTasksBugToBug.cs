using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Contracts.Task.Commands
{
    public class ChangeTasksBugToBug : IChangeChildBugToBug
    {
        [JsonIgnore]
        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public Guid TaskId { get; set; }
        [JsonIgnore]
        public Guid ChildBugId { get; set; }
    }
}

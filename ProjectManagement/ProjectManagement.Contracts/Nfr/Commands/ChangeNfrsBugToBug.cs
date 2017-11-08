using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Commands;
using System;

namespace ProjectManagement.Contracts.Nfr.Commands
{
    public class ChangeNfrsBugToBug : IChangeChildBugToBug
    {
        [JsonIgnore]
        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public Guid NfrId { get; set; }
        [JsonIgnore]
        public Guid ChildBugId { get; set; }
    }
}

using System;
using Infrastructure.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Project.Commands
{
    public class AssignUserToProject : ICommand
    {
        public AssignUserToProject(Guid adminId, Guid userToAssignId, long projectVersion)
        {
            AdminId = adminId;
            UserToAssignId = userToAssignId;
            ProjectVersion = projectVersion;
        }

        public Guid AdminId { get; private set; }
        [JsonIgnore]
        public Guid ProjectId { get; set; }
        public Guid UserToAssignId { get; private set; }
        public long ProjectVersion { get; private set; }
    }
}

using System;
using ProjectManagement.Infrastructure.Primitives.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Project.Commands
{
    public class AssignUserToProject : ICommand
    {
        public AssignUserToProject(Guid userToAssignId, long projectVersion)
        {
            UserToAssignId = userToAssignId;
            ProjectVersion = projectVersion;
        }

        [JsonIgnore]
        public Guid ProjectId { get; set; }
        public Guid UserToAssignId { get; private set; }
        public long ProjectVersion { get; private set; }
    }
}

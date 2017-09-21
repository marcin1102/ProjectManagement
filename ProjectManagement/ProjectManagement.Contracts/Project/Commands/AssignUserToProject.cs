using System;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Project.Commands
{
    public class AssignUserToProject : ICommand
    {
        public AssignUserToProject(Guid adminId, Guid projectId, Guid userToAssignId, long projectVersion)
        {
            AdminId = adminId;
            ProjectId = projectId;
            UserToAssignId = userToAssignId;
            ProjectVersion = projectVersion;
        }

        public Guid AdminId { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid UserToAssignId { get; private set; }
        public long ProjectVersion { get; private set; }
    }
}

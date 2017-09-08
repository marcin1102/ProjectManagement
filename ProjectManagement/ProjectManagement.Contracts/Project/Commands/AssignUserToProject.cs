using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Contracts.Interfaces;

namespace ProjectManagement.Contracts.Project.Commands
{
    public class AssignUserToProject : IAuthorizationRequiredCommand
    {
        public AssignUserToProject(Guid userId, Guid projectId, Guid userToAssignId, long projectVersion)
        {
            UserId = userId;
            ProjectId = projectId;
            UserToAssignId = userToAssignId;
            ProjectVersion = projectVersion;
        }

        public Guid UserId { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid UserToAssignId { get; private set; }
        public long ProjectVersion { get; private set; }
    }
}

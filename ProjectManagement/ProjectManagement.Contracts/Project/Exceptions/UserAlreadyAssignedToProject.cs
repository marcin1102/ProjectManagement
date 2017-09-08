using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Contracts.Project.Exceptions
{
    public class UserAlreadyAssignedToProject : Exception
    {
        public UserAlreadyAssignedToProject(Guid userId, Guid projectId)
            : base($"User with id {userId} is already assigned to Project with id {projectId}")
        {
            UserId = userId;
            ProjectId = projectId;
        }

        public Guid ProjectId { get; private set; }
        public Guid UserId { get; private set; }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.ProjectUser
{
    public class ProjectUser
    {
        public ProjectUser() { }

        public ProjectUser(Guid projectId, Guid userId)
        {
            Id = Guid.NewGuid();
            ProjectId = projectId;
            UserId = userId;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public Guid UserId { get; private set; }
    }
}

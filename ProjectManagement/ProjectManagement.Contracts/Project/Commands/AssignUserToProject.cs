using System;
using ProjectManagement.Infrastructure.Primitives.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Project.Commands
{
    public class AssignUserToProject : ICommand
    {
        public AssignUserToProject(Guid userToAssignId)
        {
            UserToAssignId = userToAssignId;
        }

        [JsonIgnore]
        public Guid ProjectId { get; set; }
        public Guid UserToAssignId { get; private set; }
    }
}

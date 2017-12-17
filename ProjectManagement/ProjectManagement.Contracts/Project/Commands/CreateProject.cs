using System;
using ProjectManagement.Infrastructure.Primitives.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Project.Commands
{
    public class CreateProject : ICommand
    {
        public CreateProject(Guid adminId, string name)
        {
            AdminId = adminId;
            Name = name;
        }

        public Guid AdminId { get; private set; }
        public string Name { get; private set; }

        [JsonIgnore]
        public Guid CreatedId { get; set; }
    }
}

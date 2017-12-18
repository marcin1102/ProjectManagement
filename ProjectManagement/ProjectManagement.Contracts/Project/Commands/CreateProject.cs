using System;
using ProjectManagement.Infrastructure.Primitives.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Project.Commands
{
    public class CreateProject : ICommand
    {
        public CreateProject(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        [JsonIgnore]
        public Guid CreatedId { get; set; }
    }
}

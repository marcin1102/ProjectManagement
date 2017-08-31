using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Project.Commands
{
    public class CreateProject : ICommand
    {
        public CreateProject(string name, Guid createdId)
        {
            Name = name;
            CreatedId = createdId;
        }

        public string Name { get; private set; }
        [JsonIgnore]
        public Guid CreatedId { get; set; }
    }
}

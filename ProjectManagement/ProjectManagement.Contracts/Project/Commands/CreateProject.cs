using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Interfaces;

namespace ProjectManagement.Contracts.Project.Commands
{
    public class CreateProject : IAuthorizationRequiredCommand
    {
        public CreateProject(Guid userId, string name)
        {
            UserId = userId;
            Name = name;
        }

        public Guid UserId { get; private set; }
        public string Name { get; private set; }

        [JsonIgnore]
        public Guid CreatedId { get; set; }
    }
}

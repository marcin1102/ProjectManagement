using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;

namespace ProjectManagement.Label
{
    public class Label : IEntity
    {
        private Label()
        { }
        public Label(Guid id, Guid projectId, string name, string description)
        {
            Id = id;
            ProjectId = projectId;
            Name = name;
            Description = description;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
    }
}

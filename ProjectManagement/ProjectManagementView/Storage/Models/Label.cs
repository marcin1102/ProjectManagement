using System;
using Infrastructure.Storage.EF;

namespace ProjectManagementView.Storage.Models
{
    public class Label : IEntity
    {
        public Label(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

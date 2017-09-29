using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;

namespace ProjectManagementView.Storage.Models
{
    public class Project : IEntity
    {
        public Project(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        public ICollection<Sprint> Sprints { get; set; }
        public ICollection<User> Users { get; set; }
        public long Version { get; set; }
    }
}

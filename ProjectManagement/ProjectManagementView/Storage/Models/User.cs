using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Storage.EF;

namespace ProjectManagementView.Storage.Models
{
    public class User : IEntity
    {
        public User(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Version { get; set; }
    }
}

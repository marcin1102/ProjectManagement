using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Infrastructure.Storage.EF;
using UserManagement.Contracts.User.Enums;

namespace ProjectManagementView.Storage.Models
{
    public class User : IEntity
    {
        private User()
        {

        }

        public User(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public long Version { get; set; }
    }
}

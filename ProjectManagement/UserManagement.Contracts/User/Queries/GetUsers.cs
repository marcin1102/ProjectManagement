﻿using ProjectManagement.Infrastructure.Primitives.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Contracts.User.Queries
{
    public class GetUsers : IQuery<IReadOnlyCollection<UserListItem>>
    {
        
    }

    public class UserListItem
    {
        public UserListItem(Guid id, string firstName, string lastName, string email, string role, long version)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Role = role;
            Version = version;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Role { get; private set; }
        public long Version { get; private set; }
    }
}

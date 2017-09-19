﻿using System;
using Infrastructure.Message;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UserManagement.Contracts.User.Enums;

namespace UserManagement.Contracts.User.Queries
{
    public class GetUser : IQuery<UserResponse>
    {
        public GetUser(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    public class UserResponse
    {
        public UserResponse(Guid id, string firstName, string lastName, string email, string role, long version)
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
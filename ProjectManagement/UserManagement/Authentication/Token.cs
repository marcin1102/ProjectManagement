﻿using ProjectManagement.Infrastructure.Storage.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Authentication
{
    public class Token
    {
        private Token() { }
        public Token(string value, Guid userId)
        {
            Value = value;
            UserId = userId;
            LastlyUsed = DateTimeOffset.UtcNow;
        }

        public string Value { get; private set; }
        public Guid UserId { get; private set; }
        public DateTimeOffset LastlyUsed { get; private set; }

        public void Update()
        {
            LastlyUsed = DateTimeOffset.UtcNow;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Infrastructure.CallContexts
{
    public class CallContext
    {
        public Guid UserId { get; private set; }

        public void SetUserId(Guid id)
        {
            UserId = id;
        }
    }
}

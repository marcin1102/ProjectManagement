﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Bug.Commands
{
    public class AssignAssigneeToBug : AssignAssigneeToIssue
    {
        public AssignAssigneeToBug(Guid userId, Guid assigneeId) : base(userId, assigneeId)
        {
        }
    }
}

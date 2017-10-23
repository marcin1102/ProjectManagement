﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Bug.Commands
{
    public class MarkBugAsInProgress : MarkAsInProgress
    {
        public MarkBugAsInProgress(Guid bugId, Guid userId) : base(bugId, userId)
        {
        }
    }
}
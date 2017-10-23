﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Issue.Commands;

namespace ProjectManagement.Contracts.Subtask.Commands
{
    public class CommentSubtask : CommentIssue
    {
        public CommentSubtask(Guid subtaskId, Guid memberId, string content) : base(subtaskId, memberId, content)
        {
        }
    }
}
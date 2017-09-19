﻿using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Contracts.Issue.Commands;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Tests.Infrastructure;

namespace ProjectManagement.Tests.Issue
{
    public static class IssueExtensions
    {
        private static Random random = new Random();
        public static CreateIssue GenerateBasicCreateIssueCommand(SeededData data, IssueType type)
        {
            var title = "TITLE_" + random.Next(100000, 999999);
            var description = "DESC" + random.Next(100000, 999999);
            return new CreateIssue(data.ProjectId, title, description, type, data.UserAssignedToProjectId, null, null);
        }

        public static void WithAssignee(this CreateIssue command, Guid assigneeId)
        {
            command.SetAssignee(assigneeId);
        }

        public static void WithLabels(this CreateIssue command, ICollection<Guid> labelsIds)
        {
            command.SetLabels(labelsIds);
        }
    }
}
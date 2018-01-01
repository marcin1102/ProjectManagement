using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Contracts.Issue.Commands;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Nfr.Commands;
using ProjectManagement.Contracts.Task.Commands;
using ProjectManagement.Tests.Infrastructure;

namespace ProjectManagement.Tests.Issue
{
    public static class IssueExtensions
    {
        private static Random random = new Random();
        public static CreateTask GenerateBasicCreateTaskCommand(SeededData data)
        {
            var title = "TITLE_" + random.Next(100000, 999999);
            var description = "DESC" + random.Next(100000, 999999);
            return new CreateTask(title, description, null, null)
            {
                ProjectId = data.ProjectId
            };
        }

        public static CreateNfr GenerateBasicCreateNfrCommand(SeededData data)
        {
            var title = "TITLE_" + random.Next(100000, 999999);
            var description = "DESC" + random.Next(100000, 999999);
            return new CreateNfr(title, description, null, null)
            {
                ProjectId = data.ProjectId
            };
        }

        public static IAddBugTo GenerateBasicAddBugToCommand(SeededData data, IssueType issueType, Guid? parentIssueId = null)
        {
            var title = "TITLE_" + random.Next(100000, 999999);
            var description = "DESC" + random.Next(100000, 999999);
            Guid issueId;
            if (parentIssueId == null)
                issueId = issueType == IssueType.Nfr ? data.NfrId : data.TaskId;

            if(issueType == IssueType.Nfr)
                return new AddBugToNfr(title, description, null, null)
                {
                    ProjectId = data.ProjectId,
                    NfrId = issueId
                };
            else
                return new AddBugToTask(title, description, null, null)
                {
                    ProjectId = data.ProjectId,
                    TaskId = issueId
                };
        }

        public static ICreateIssue WithAssignee(this ICreateIssue command, Guid assigneeId)
        {
            command.SetAssignee(assigneeId);
            return command;
        }

        public static ICreateIssue WithLabels(this ICreateIssue command, ICollection<Guid> labelsIds)
        {
            command.SetLabels(labelsIds);
            return command;
        }
    }
}

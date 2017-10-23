using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagement.Contracts.Issue.Commands;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Task.Commands;
using ProjectManagement.Tests.Infrastructure;

namespace ProjectManagement.Tests.Issue
{
    public static class IssueExtensions
    {
        //private static Random random = new Random();
        //public static TCreateIssueCommand GenerateBasicCreateIssueCommand<TCreateIssueCommand>(SeededData data, IssueType type)
        //    where TCreateIssueCommand : ICreateIssue
        //{
        //    var title = "TITLE_" + random.Next(100000, 999999);
        //    var description = "DESC" + random.Next(100000, 999999);
        //    return new CreateTask(data.ProjectId, title, description, data.UserAssignedToProjectId, null, null, null);
        //}

        //public static ICreateIssue WithAssignee(this ICreateIssue command, Guid assigneeId)
        //{
        //    command.SetAssignee(assigneeId);
        //    return command;
        //}

        //public static ICreateIssue WithLabels(this ICreateIssue command, ICollection<Guid> labelsIds)
        //{
        //    command.SetLabels(labelsIds);
        //    return command;
        //}

        ////public static ICreateIssue WithSubtasks(this ICreateIssue command, ICollection<Guid> subtasksIds)
        ////{
        ////    command.SetSubtasks(subtasksIds);
        ////    return command;
        ////}
    }
}

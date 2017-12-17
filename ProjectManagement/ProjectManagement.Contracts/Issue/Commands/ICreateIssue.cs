using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using ProjectManagement.Infrastructure.Primitives.Message;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Issue.Enums;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public interface ICreateIssue : ICommand
    {
         Guid ProjectId { get;}
         string Title { get; }
         string Description { get; }
         Guid ReporterId { get;  }
         Guid? AssigneeId { get; }
         ICollection<Guid> LabelsIds { get; }

        [JsonIgnore]
        Guid CreatedId { get; set; }

        void SetAssignee(Guid assigneeId);
        void SetLabels(ICollection<Guid> labelsIds);
    }

    public interface IAddRelatedIssueToTask : ICreateIssue { }
    public interface IAddBugTo : IAddRelatedIssueToTask { }
    public interface IAddSubtaskTo : IAddRelatedIssueToTask { }
}

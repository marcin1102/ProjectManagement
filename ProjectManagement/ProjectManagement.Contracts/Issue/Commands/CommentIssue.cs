using System;
using FluentValidation;
using ProjectManagement.Infrastructure.Primitives.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Issue.Commands
{
    /// <summary>
    /// Should be implemented as independent logic. Right now looks like optimistic concurrency as hell.
    /// </summary>
    public class CommentIssue : ICommand
    {
        public CommentIssue(string content)
        {
            Content = content;
        }

        [JsonIgnore]
        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public Guid IssueId { get; set; }
        public string Content { get; private set; }
    }

    public class CommentIssueValidator : AbstractValidator<CommentIssue>
    {
        public CommentIssueValidator()
        {
        }
    }
}

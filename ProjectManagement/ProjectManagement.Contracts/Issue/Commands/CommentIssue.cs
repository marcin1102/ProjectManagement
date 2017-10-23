using System;
using FluentValidation;
using Infrastructure.Message;
using Newtonsoft.Json;

namespace ProjectManagement.Contracts.Issue.Commands
{
    /// <summary>
    /// Should be implemented as independent logic. Right now looks like optimistic concurrency as hell.
    /// </summary>
    public class CommentIssue : ICommand
    {
        public CommentIssue(Guid memberId, string content)
        {
            MemberId = memberId;
            Content = content;
        }

        [JsonIgnore]
        public Guid ProjectId { get; set; }
        [JsonIgnore]
        public Guid IssueId { get; set; }
        public Guid MemberId { get; private set; }
        public string Content { get; private set; }
    }

    public class CommentIssueValidator : AbstractValidator<CommentIssue>
    {
        public CommentIssueValidator()
        {
        }
    }
}

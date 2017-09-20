using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Infrastructure.Message;
using ProjectManagement.Contracts.Issue.Comment;

namespace ProjectManagement.Contracts.Issue.Commands
{
    public class CommentIssue : ICommand
    {
        public CommentIssue(Guid issueId, Comment.Comment comment)
        {
            IssueId = issueId;
            Comment = comment;
        }

        public Guid IssueId { get; private set; }
        public Comment.Comment Comment { get; private set; }
    }

    public class CommentIssueValidator : AbstractValidator<CommentIssue>
    {
        public CommentIssueValidator()
        {
            RuleFor(x => x.IssueId).NotNull();
            RuleFor(x => x.Comment).NotNull()
                .Must(MemberIdNotNull).Must(ContentNotEmpty);

        }

        private bool MemberIdNotNull(Comment.Comment comment)
        {
            return comment.MemberId != null;
        }

        private bool ContentNotEmpty(Comment.Comment comment)
        {
            return !string.IsNullOrEmpty(comment.Content);
        }
    }
}

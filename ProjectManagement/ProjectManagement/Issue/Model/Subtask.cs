using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Exceptions;
using Infrastructure.Storage.EF;
using ProjectManagement.Contracts.DomainExceptions;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Issue.Model.Abstract;
using ProjectManagement.Providers;
using ProjectManagement.Services;
using ProjectManagement.Sprint.Searchers;

namespace ProjectManagement.Issue.Model
{
    public class Subtask : Abstract.Issue
    {
        private Subtask() : base() { }
        public Subtask(Guid id, Guid projectId, string title, string description, IssueStatus status, Guid reporterId, Guid? assigneeId, DateTime createdAt, DateTime updatedAt) :
            base(id, projectId, title, description, status, reporterId, assigneeId, createdAt, updatedAt)
        {
        }
    }
}

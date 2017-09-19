using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using ProjectManagement.Contracts.Issue.Commands;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Issue.Model;
using ProjectManagement.Label.Searcher;
using ProjectManagement.User.Repository;

namespace ProjectManagement.Issue.Factory
{
    public interface IIssueFactory
    {
        Task<Model.Issue> GenerateIssue(CreateIssue command);
    }
    public class IssueFactory : IIssueFactory
    {
        private readonly UserRepository userRepository;
        private readonly ILabelsSearcher labelsSearcher;

        public IssueFactory(UserRepository userRepository, ILabelsSearcher labelsSearcher)
        {
            this.userRepository = userRepository;
            this.labelsSearcher = labelsSearcher;
        }

        public async Task<Model.Issue> GenerateIssue(CreateIssue command)
        {
            var reporter = await userRepository.GetAsync(command.ReporterId);
            var assignee = command.AssigneeId.HasValue ? await userRepository.GetAsync(command.AssigneeId.Value) : null;
            return new Model.Issue(
                id: Guid.NewGuid(),
                projectId: command.ProjectId,
                title: command.Title,
                description: command.Description,
                type: command.Type,
                status: Status.Todo,
                reporter: reporter,
                assignee: assignee,
                createdAt: DateTime.Now,
                updatedAt: DateTime.Now
                );
        }
    }
}

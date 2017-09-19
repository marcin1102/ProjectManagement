using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Issue.Commands;
using ProjectManagement.Issue.Factory;
using ProjectManagement.Issue.Repository;
using ProjectManagement.Label.Searcher;
using ProjectManagement.Project.Repository;

namespace ProjectManagement.Issue.Handlers
{
    public class IssueCommandHandler :
        IAsyncCommandHandler<CreateIssue>
    {
        private readonly IssueRepository repository;
        private readonly IIssueFactory issueFactory;
        private readonly ProjectRepository projectRepository;
        private readonly ILabelsSearcher labelsSearcher;

        public IssueCommandHandler(IssueRepository repository, IIssueFactory issueFactory, ProjectRepository projectRepository, ILabelsSearcher labelsSearcher)
        {
            this.repository = repository;
            this.issueFactory = issueFactory;
            this.projectRepository = projectRepository;
            this.labelsSearcher = labelsSearcher;
        }

        public async Task HandleAsync(CreateIssue command)
        {
            if (await projectRepository.FindAsync(command.ProjectId) == null)
                throw new EntityDoesNotExist(command.ProjectId, nameof(Project.Model.Project));

            var issue = await issueFactory.GenerateIssue(command);

            if (command.LabelsIds != null)
            {
                await ValidateLabelsExisting(command.LabelsIds);
                issue.AssignLabels(command.LabelsIds);
            }

            issue.Created();
            await repository.AddAsync(issue, issue.Version);
            command.CreatedId = issue.Id;
        }

        private async Task ValidateLabelsExisting(ICollection<Guid> labelsIds)
        {
            var AreLabelsExist = await labelsSearcher.CheckIfLabelsExist(labelsIds);
            foreach (var item in AreLabelsExist)
            {
                if (!item.Value)
                    throw new EntityDoesNotExist(item.Key, nameof(Label.Label));
            }
        }
    }
}

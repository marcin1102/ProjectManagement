using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Exceptions;
using ProjectManagement.Contracts.Issue.Commands;
using ProjectManagement.Issue.Factory;
using ProjectManagement.Issue.Repository;
using ProjectManagement.Issue.Searchers;
using ProjectManagement.Label.Searcher;
using ProjectManagement.Project.Repository;
using ProjectManagement.Project.Searchers;
using ProjectManagement.Providers;

namespace ProjectManagement.Issue.Handlers
{
    public class IssueCommandHandler :
        IAsyncCommandHandler<CreateIssue>,
        IAsyncCommandHandler<AssignLabelsToIssue>,
        IAsyncCommandHandler<CommentIssue>,
        IAsyncCommandHandler<AddSubtask>
    {
        private readonly IssueRepository repository;
        private readonly IIssueFactory issueFactory;
        private readonly ProjectRepository projectRepository;
        private readonly ILabelsSearcher labelsSearcher;
        private readonly IProjectSearcher projectSearcher;
        private readonly IIssueSearcher searcher;

        public IssueCommandHandler(IssueRepository repository, IIssueFactory issueFactory, ProjectRepository projectRepository, ILabelsSearcher labelsSearcher, IProjectSearcher projectSearcher, IIssueSearcher searcher)
        {
            this.repository = repository;
            this.issueFactory = issueFactory;
            this.projectRepository = projectRepository;
            this.labelsSearcher = labelsSearcher;
            this.projectSearcher = projectSearcher;
            this.searcher = searcher;
        }

        public async Task HandleAsync(CreateIssue command)
        {
            await ValidateProjectAndReporter(command.ProjectId, command.ReporterId);

            var issue = await issueFactory.GenerateIssue(command);

            if (command.LabelsIds != null)
            {
                await ValidateLabelsExisting(issue.ProjectId, command.LabelsIds);
                issue.AssignLabels(command.LabelsIds);
            }

            issue.Created();
            await repository.AddAsync(issue, issue.Version);
            command.CreatedId = issue.Id;
        }

        private async Task ValidateProjectAndReporter(Guid projectId, Guid reporterId)
        {
            if (await projectRepository.FindAsync(projectId) == null)
                throw new EntityDoesNotExist(projectId, nameof(Project.Model.Project));

            await ValidateUserMembership(projectId, reporterId);
        }

        public async Task HandleAsync(AssignLabelsToIssue command)
        {
            var issue = await repository.GetAsync(command.IssueId);
            await ValidateLabelsExisting(issue.ProjectId, command.LabelsIds);
            issue.AssignLabels(command.LabelsIds);
            await repository.Update(issue, issue.Version);
        }

        public async Task HandleAsync(CommentIssue command)
        {
            var issue = await repository.GetAsync(command.IssueId);
            await ValidateUserMembership(issue.ProjectId, command.Comment.MemberId);
            issue.Comment(command.Comment);
            await repository.Update(issue, issue.Version);
        }

        private async Task ValidateUserMembership(Guid projectId, Guid memberId)
        {
            var isUserProjectMember = await projectSearcher.IsUserProjectMember(projectId, memberId);
            if (!isUserProjectMember)
                throw new UserIsNotProjectMember(memberId, projectId);
        }

        private async Task ValidateLabelsExisting(Guid projectId, ICollection<Guid> labelsIds)
        {
            var AreLabelsExist = await labelsSearcher.CheckIfLabelsExist(projectId, labelsIds);
            foreach (var item in AreLabelsExist)
            {
                if (!item.Value)
                    throw new EntityDoesNotExist(item.Key, nameof(Label.Label));
            }
        }

        public async Task HandleAsync(AddSubtask command)
        {
            var issue = await repository.GetAsync(command.IssueId);
            var doesSubtaskExistInProject = await searcher.DoesIssueExistInProject(command.SubtaskId, issue.ProjectId);
            if (!doesSubtaskExistInProject)
                throw new EntityDoesNotExistsInScope(command.SubtaskId, nameof(Model.Issue), nameof(Project.Model.Project), issue.ProjectId);

            var subtask = await repository.GetAsync(command.SubtaskId);

            issue.AddSubtask(command.SubtaskId, subtask.Type);
            await repository.Update(issue, issue.Version);
        }
    }
}

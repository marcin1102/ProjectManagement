using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.DomainExceptions;
using ProjectManagement.Contracts.Issue.Commands;
using ProjectManagement.Issue.Factory;
using ProjectManagement.Issue.Repository;
using ProjectManagement.Issue.Searchers;
using ProjectManagement.Label.Searcher;
using ProjectManagement.Project.Repository;
using ProjectManagement.Project.Searchers;
using ProjectManagement.Services;
using ProjectManagement.User.Repository;
using ProjectManagement.Sprint.Searchers;

namespace ProjectManagement.Issue.Handlers
{
    public class IssueCommandHandler :
        IAsyncCommandHandler<CreateIssue>,
        IAsyncCommandHandler<AssignLabelsToIssue>,
        IAsyncCommandHandler<CommentIssue>,
        IAsyncCommandHandler<AddSubtask>,
        IAsyncCommandHandler<MarkAsInProgress>,
        IAsyncCommandHandler<MarkAsDone>,
        IAsyncCommandHandler<AssignAssigneeToIssue>,
        IAsyncCommandHandler<AssignIssueToSprint>
    {
        private readonly IssueRepository issueRepository;
        private readonly IIssueFactory issueFactory;
        private readonly ProjectRepository projectRepository;
        private readonly ILabelsSearcher labelsSearcher;
        private readonly IIssueSearcher issueSearcher;
        private readonly IAuthorizationService authorizationService;
        private readonly UserRepository userRepository;
        private readonly ISprintSearcher sprintSearcher;

        public IssueCommandHandler(IssueRepository issueRepository, IIssueFactory issueFactory, ProjectRepository projectRepository, ILabelsSearcher labelsSearcher, IIssueSearcher issueSearcher, IAuthorizationService authorizationService, UserRepository userRepository, ISprintSearcher sprintSearcher)
        {
            this.issueRepository = issueRepository;
            this.issueFactory = issueFactory;
            this.projectRepository = projectRepository;
            this.labelsSearcher = labelsSearcher;
            this.issueSearcher = issueSearcher;
            this.authorizationService = authorizationService;
            this.userRepository = userRepository;
            this.sprintSearcher = sprintSearcher;
        }

        public async Task HandleAsync(CreateIssue command)
        {
            if (await projectRepository.FindAsync(command.ProjectId) == null)
                throw new EntityDoesNotExist(command.ProjectId, nameof(Project.Model.Project));

            await authorizationService.CheckUserMembership(command.ReporterId, command.ProjectId);

            var issue = await issueFactory.GenerateIssue(command);

            if (command.LabelsIds != null)
            {
                await ValidateLabelsExistence(issue.ProjectId, command.LabelsIds);
                issue.AssignLabels(command.LabelsIds);
            }

            if(command.SubtasksIds != null)
            {
                await ValidateIssuesExistence(command.ProjectId, command.SubtasksIds);
                issue.AddSubtasks(command.SubtasksIds);
            }

            issue.Created();
            await issueRepository.AddAsync(issue);
            command.CreatedId = issue.Id;
        }

        private async Task ValidateIssuesExistence(Guid projectId, ICollection<Guid> subtasksIds)
        {
            var issuesThatDoNotExist = await issueSearcher.DoesIssuesExistInProject(projectId, subtasksIds);
            if (issuesThatDoNotExist.Count != 0)
                throw new EntitiesDoesNotExistInScope(issuesThatDoNotExist, nameof(Model.Issue), nameof(Project.Model.Project), projectId);
        }

        public async Task HandleAsync(AssignLabelsToIssue command)
        {
            var issue = await issueRepository.GetAsync(command.IssueId);
            await ValidateLabelsExistence(issue.ProjectId, command.LabelsIds);
            issue.AssignLabels(command.LabelsIds);
            await issueRepository.Update(issue, issue.Version);
        }

        public async Task HandleAsync(CommentIssue command)
        {
            var issue = await issueRepository.GetAsync(command.IssueId);
            await authorizationService.CheckUserMembership(command.Comment.MemberId, issue.ProjectId);
            issue.Comment(command.Comment);
            await issueRepository.Update(issue, issue.Version);
        }

        private async Task ValidateLabelsExistence(Guid projectId, ICollection<Guid> labelsIds)
        {
            var labelsThatDoNotExist = await labelsSearcher.DoesLabelsExistInScope(projectId, labelsIds);
            if (labelsThatDoNotExist.Count != 0)
                throw new EntitiesDoesNotExistInScope(labelsThatDoNotExist, nameof(Label.Label), nameof(Project.Model.Project), projectId);
        }

        public async Task HandleAsync(AddSubtask command)
        {
            var issue = await issueRepository.GetAsync(command.IssueId);
            var originaVersion = issue.Version;
            var doesSubtaskExistInProject = await issueSearcher.DoesIssueExistInProject(command.SubtaskId, issue.ProjectId);
            if (!doesSubtaskExistInProject)
                throw new EntityDoesNotExistsInScope(command.SubtaskId, nameof(Model.Issue), nameof(Project.Model.Project), issue.ProjectId);

            var subtask = await issueRepository.GetAsync(command.SubtaskId);

            issue.AddSubtask(command.SubtaskId, subtask.Type);
            await issueRepository.Update(issue, originaVersion);
        }

        public async Task HandleAsync(MarkAsInProgress command)
        {
            var issue = await issueRepository.GetAsync(command.IssueId);
            var originalVersion = issue.Version;
            issue.MarkAsInProgress();
            await issueRepository.Update(issue, originalVersion);
        }

        public async Task HandleAsync(MarkAsDone command)
        {
            var issue = await issueRepository.GetAsync(command.IssueId);
            var originalVersion = issue.Version;
            await authorizationService.CheckUserMembership(command.UserId, issue.ProjectId);

            var relatedIssuesStatuses = await issueSearcher.GetRelatedIssuesStatuses(issue.Id);
            issue.MarkAsDone(relatedIssuesStatuses);
            await issueRepository.Update(issue, originalVersion);
        }

        public async Task HandleAsync(AssignAssigneeToIssue command)
        {
            var issue = await issueRepository.GetAsync(command.IssueId);
            var originalVersion = issue.Version;
            await authorizationService.CheckUserMembership(command.UserId, issue.ProjectId);
            var assignee = await userRepository.GetAsync(command.UserId);

            issue.AssignAssignee(assignee);
            await issueRepository.Update(issue, originalVersion);
        }

        public async Task HandleAsync(AssignIssueToSprint command)
        {
            var issue = await issueRepository.GetAsync(command.IssueId);            
            var doesSprintExist = await sprintSearcher.DoesSprintExistInScope(command.SprintId, issue.ProjectId);
            if (!doesSprintExist)
                throw new EntityDoesNotExistsInScope(command.SprintId, nameof(Sprint.Model.Sprint), nameof(Project.Model.Project), issue.ProjectId);

            var originalVersion = issue.Version;
            issue.AssignToSprint(command.SprintId);
            await issueRepository.Update(issue, originalVersion);
        }
    }
}

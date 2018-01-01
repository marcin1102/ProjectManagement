using System;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Issue.Commands;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Nfr.Commands;
using ProjectManagement.Contracts.Task.Commands;
using ProjectManagement.Label.Searcher;
using ProjectManagement.Services;
using ProjectManagement.Sprint.Repository;
using ProjectManagement.User.Repository;
using ProjectManagement.Project.Repository;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Contracts.Bug.Commands;
using ProjectManagement.Issue.Model.Abstract;
using ProjectManagement.Infrastructure.CallContexts;

namespace ProjectManagement.Issue.Factory
{
    public interface IIssueFactory
    {
        Task<Model.ChildBug> GenerateChildBug(IAddBugTo command);
        Task<Model.Subtask> GenerateSubtask(AddSubtaskToTask command);
        Task<AggregateIssue> Create(ICreateAggregateIssue command);
    }
    public class IssueFactory : IIssueFactory
    {
        private readonly UserRepository userRepository;
        private readonly ILabelsSearcher labelsSearcher;
        private readonly SprintRepository sprintRepository;
        private readonly IMembershipService authorizationService;
        private readonly ProjectRepository projectRepository;
        private readonly CallContext callContext;

        public IssueFactory(UserRepository userRepository, ILabelsSearcher labelsSearcher, SprintRepository sprintRepository, IMembershipService authorizationService, ProjectRepository projectRepository, CallContext callContext)
        {
            this.userRepository = userRepository;
            this.labelsSearcher = labelsSearcher;
            this.sprintRepository = sprintRepository;
            this.authorizationService = authorizationService;
            this.projectRepository = projectRepository;
            this.callContext = callContext;
        }

        public async Task<Model.ChildBug> GenerateChildBug(IAddBugTo command)
        {
            await authorizationService.CheckUserMembership(callContext.UserId, command.ProjectId);

            var issue = new Model.ChildBug(
                                id: Guid.NewGuid(),
                                projectId: command.ProjectId,
                                title: command.Title,
                                description: command.Description,
                                status: IssueStatus.Todo,
                                reporterId: callContext.UserId,
                                assigneeId: null,
                                createdAt: DateTime.UtcNow,
                                updatedAt: DateTime.UtcNow
                             );

            if (command.LabelsIds != null)
            {
                var labels = await labelsSearcher.GetLabels(command.ProjectId);
                issue.AssignLabels(command.LabelsIds, labels);
            }

            if (command.AssigneeId != null)
            {
                var assignee = await userRepository.GetAsync(command.AssigneeId.Value);
                await issue.AssignAssignee(assignee, authorizationService);
            }

            command.CreatedId = issue.Id;
            return issue;
        }

        public async Task<Model.Subtask> GenerateSubtask(AddSubtaskToTask command)
        {
            await authorizationService.CheckUserMembership(callContext.UserId, command.ProjectId);

            var issue = new Model.Subtask(
                                id: Guid.NewGuid(),
                                projectId: command.ProjectId,
                                title: command.Title,
                                description: command.Description,
                                status: IssueStatus.Todo,
                                reporterId: callContext.UserId,
                                assigneeId: null,
                                createdAt: DateTime.UtcNow,
                                updatedAt: DateTime.UtcNow
                             );

            if (command.LabelsIds != null)
            {
                var labels = await labelsSearcher.GetLabels(command.ProjectId);
                issue.AssignLabels(command.LabelsIds, labels);
            }

            if (command.AssigneeId != null)
            {
                var assignee = await userRepository.GetAsync(command.AssigneeId.Value);
                await issue.AssignAssignee(assignee, authorizationService);
            }

            command.CreatedId = issue.Id;
            return issue;
        }

        private async Task CheckIfProjectExist(Guid projectId)
        {
            if (await projectRepository.FindAsync(projectId) == null)
                throw new EntityDoesNotExist(projectId, nameof(Project.Model.Project));
        }

        public async Task<AggregateIssue> Create(ICreateAggregateIssue command)
        {
            await CheckIfProjectExist(command.ProjectId);
            await authorizationService.CheckUserMembership(callContext.UserId, command.ProjectId);
            AggregateIssue issue;
            switch (command)
            {
                case CreateTask createTask:
                    issue = new Model.Task(
                                id: Guid.NewGuid(),
                                projectId: command.ProjectId,
                                title: command.Title,
                                description: command.Description,
                                status: IssueStatus.Todo,
                                reporterId: callContext.UserId,
                                assigneeId: null,
                                createdAt: DateTime.UtcNow,
                                updatedAt: DateTime.UtcNow
                             );
                    break;
                case CreateNfr createNfr:
                    issue = new Model.Nfr(
                                id: Guid.NewGuid(),
                                projectId: command.ProjectId,
                                title: command.Title,
                                description: command.Description,
                                status: IssueStatus.Todo,
                                reporterId: callContext.UserId,
                                assigneeId: null,
                                createdAt: DateTime.UtcNow,
                                updatedAt: DateTime.UtcNow
                             );
                    break;
                case CreateBug createBug:
                    issue = new Model.Bug(
                                id: Guid.NewGuid(),
                                projectId: command.ProjectId,
                                title: command.Title,
                                description: command.Description,
                                status: IssueStatus.Todo,
                                reporterId: callContext.UserId,
                                assigneeId: null,
                                createdAt: DateTime.UtcNow,
                                updatedAt: DateTime.UtcNow
                             );
                    break;
                default:
                    throw new InvalidCastException("Cannot cast command to Create Aggregate Issue command");
            }

            issue.Created();

            if (command.LabelsIds != null)
            {
                var labels = await labelsSearcher.GetLabels(command.ProjectId);
                issue.AssignLabels(command.LabelsIds, labels);
            }

            if (command.AssigneeId != null)
            {
                var assignee = await userRepository.GetAsync(command.AssigneeId.Value);
                await issue.AssignAssignee(assignee, authorizationService);
            }

            command.CreatedId = issue.Id;
            return issue;
        }
    }
}

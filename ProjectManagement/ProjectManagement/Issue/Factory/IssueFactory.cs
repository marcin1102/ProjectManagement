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
using Infrastructure.Exceptions;
using ProjectManagement.Contracts.Bug.Commands;

namespace ProjectManagement.Issue.Factory
{
    public interface IIssueFactory
    {
        Task<Model.Task> GenerateTask(CreateTask command);
        Task<Model.Nfr> GenerateNfr(CreateNfr command);
        Task<Model.ChildBug> GenerateChildBug<TAddBugTo>(TAddBugTo command)
            where TAddBugTo : class, IAddBugTo;
        Task<Model.Subtask> GenerateSubtask(AddSubtaskToTask command);
    }
    public class IssueFactory : IIssueFactory
    {
        private readonly UserRepository userRepository;
        private readonly ILabelsSearcher labelsSearcher;
        private readonly SprintRepository sprintRepository;
        private readonly IAuthorizationService authorizationService;
        private readonly ProjectRepository projectRepository;

        public IssueFactory(UserRepository userRepository, ILabelsSearcher labelsSearcher, SprintRepository sprintRepository, IAuthorizationService authorizationService, ProjectRepository projectRepository)
        {
            this.userRepository = userRepository;
            this.labelsSearcher = labelsSearcher;
            this.sprintRepository = sprintRepository;
            this.authorizationService = authorizationService;
            this.projectRepository = projectRepository;
        }

        public async Task<Model.Task> GenerateTask(CreateTask command)
        {
            await CheckIfProjectExist(command.ProjectId);
            await authorizationService.CheckUserMembership(command.ReporterId, command.ProjectId);

            var issue = new Model.Task(
                                id: Guid.NewGuid(),
                                projectId: command.ProjectId,
                                title: command.Title,
                                description: command.Description,
                                status: IssueStatus.Todo,
                                reporterId: command.ReporterId,
                                assigneeId: null,
                                createdAt: DateTime.Now,
                                updatedAt: DateTime.Now
                             );
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

        public async Task<Model.Nfr> GenerateNfr(CreateNfr command)
        {
            await CheckIfProjectExist(command.ProjectId);
            await authorizationService.CheckUserMembership(command.ReporterId, command.ProjectId);

            var issue = new Model.Nfr(
                                id: Guid.NewGuid(),
                                projectId: command.ProjectId,
                                title: command.Title,
                                description: command.Description,
                                status: IssueStatus.Todo,
                                reporterId: command.ReporterId,
                                assigneeId: null,
                                createdAt: DateTime.Now,
                                updatedAt: DateTime.Now
                             );

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

        public async Task<Model.Bug> GenerateBug(CreateBug command)
        {
            await CheckIfProjectExist(command.ProjectId);
            await authorizationService.CheckUserMembership(command.ReporterId, command.ProjectId);

            var issue = new Model.Bug(
                                id: Guid.NewGuid(),
                                projectId: command.ProjectId,
                                title: command.Title,
                                description: command.Description,
                                status: IssueStatus.Todo,
                                reporterId: command.ReporterId,
                                assigneeId: null,
                                createdAt: DateTime.Now,
                                updatedAt: DateTime.Now
                             );
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

        public async Task<Model.ChildBug> GenerateChildBug<TAddBugTo>(TAddBugTo command)
            where TAddBugTo : class, IAddBugTo
        {
            await authorizationService.CheckUserMembership(command.ReporterId, command.ProjectId);

            var issue = new Model.ChildBug(
                                id: Guid.NewGuid(),
                                projectId: command.ProjectId,
                                title: command.Title,
                                description: command.Description,
                                status: IssueStatus.Todo,
                                reporterId: command.ReporterId,
                                assigneeId: null,
                                createdAt: DateTime.Now,
                                updatedAt: DateTime.Now
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
            await authorizationService.CheckUserMembership(command.ReporterId, command.ProjectId);

            var issue = new Model.Subtask(
                                id: Guid.NewGuid(),
                                projectId: command.ProjectId,
                                title: command.Title,
                                description: command.Description,
                                status: IssueStatus.Todo,
                                reporterId: command.ReporterId,
                                assigneeId: null,
                                createdAt: DateTime.Now,
                                updatedAt: DateTime.Now
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
    }
}

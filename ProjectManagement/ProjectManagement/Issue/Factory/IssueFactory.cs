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

namespace ProjectManagement.Issue.Factory
{
    public interface IIssueFactory
    {
        Task<Model.Task> GenerateTask(CreateTask command);
        Task<Model.Nfr> GenerateNfr(CreateNfr command);
        Task<Model.Bug> GenerateBug<TAddBugTo>(TAddBugTo command)
            where TAddBugTo : class, IAddBugTo;
        Task<Model.Subtask> GenerateSubtask(AddSubtaskToTask command);
    }
    public class IssueFactory : IIssueFactory
    {
        private readonly UserRepository userRepository;
        private readonly ILabelsSearcher labelsSearcher;
        private readonly SprintRepository sprintRepository;
        private readonly IAuthorizationService authorizationService;

        public IssueFactory(UserRepository userRepository, ILabelsSearcher labelsSearcher, SprintRepository sprintRepository, IAuthorizationService authorizationService)
        {
            this.userRepository = userRepository;
            this.labelsSearcher = labelsSearcher;
            this.sprintRepository = sprintRepository;
            this.authorizationService = authorizationService;
        }

        public async Task<Model.Task> GenerateTask(CreateTask command)
        {
            var reporter = await userRepository.GetAsync(command.ReporterId);
            var assignee = command.AssigneeId.HasValue ? await userRepository.GetAsync(command.AssigneeId.Value) : null;
            var labels = await labelsSearcher.GetLabels(command.ProjectId);

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
                issue.AssignLabels(command.LabelsIds, labels);

            if (assignee != null)
                issue.AssignAssignee(assignee, authorizationService);

            command.CreatedId = issue.Id;
            return issue;
        }

        public async Task<Model.Nfr> GenerateNfr(CreateNfr command)
        {
            var reporter = await userRepository.GetAsync(command.ReporterId);
            var assignee = command.AssigneeId.HasValue ? await userRepository.GetAsync(command.AssigneeId.Value) : null;
            var labels = await labelsSearcher.GetLabels(command.ProjectId);

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
                issue.AssignLabels(command.LabelsIds, labels);

            if (assignee != null)
                issue.AssignAssignee(assignee, authorizationService);

            command.CreatedId = issue.Id;
            return issue;
        }

        public async Task<Model.Bug> GenerateBug<TAddBugTo>(TAddBugTo command)
            where TAddBugTo : class, IAddBugTo
        {
            var reporter = await userRepository.GetAsync(command.ReporterId);
            var assignee = command.AssigneeId.HasValue ? await userRepository.GetAsync(command.AssigneeId.Value) : null;
            var labels = await labelsSearcher.GetLabels(command.ProjectId);

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

            if (command.LabelsIds != null)
                issue.AssignLabels(command.LabelsIds, labels);

            if (assignee != null)
                issue.AssignAssignee(assignee, authorizationService);

            command.CreatedId = issue.Id;
            return issue;
        }

        public async Task<Model.Subtask> GenerateSubtask(AddSubtaskToTask command)
        {
            var reporter = await userRepository.GetAsync(command.ReporterId);
            var assignee = command.AssigneeId.HasValue ? await userRepository.GetAsync(command.AssigneeId.Value) : null;
            var labels = await labelsSearcher.GetLabels(command.ProjectId);

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
                issue.AssignLabels(command.LabelsIds, labels);

            if (assignee != null)
                issue.AssignAssignee(assignee, authorizationService);

            command.CreatedId = issue.Id;
            return issue;
        }
    }
}

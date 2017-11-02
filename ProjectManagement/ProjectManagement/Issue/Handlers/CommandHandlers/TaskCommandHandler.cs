using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Task.Commands;
using ProjectManagement.Issue.Factory;
using ProjectManagement.Issue.Repository;
using ProjectManagement.Label.Searcher;
using ProjectManagement.Project.Repository;
using ProjectManagement.Services;
using ProjectManagement.Sprint.Searchers;
using ProjectManagement.User.Repository;

namespace ProjectManagement.task.Handlers.CommandHandlers
{
    public class TaskCommandHandler :
        IAsyncCommandHandler<CreateTask>,
        IAsyncCommandHandler<AssignLabelsToTask>,
        IAsyncCommandHandler<CommentTask>,
        IAsyncCommandHandler<MarkTaskAsInProgress>,
        IAsyncCommandHandler<MarkTaskAsDone>,
        IAsyncCommandHandler<AssignAssigneeToTask>,
        IAsyncCommandHandler<AssignTaskToSprint>,
        IAsyncCommandHandler<AddBugToTask>,
        IAsyncCommandHandler<AssignLabelsToTasksBug>,
        IAsyncCommandHandler<CommentTasksBug>,
        IAsyncCommandHandler<MarkTasksBugAsInProgress>,
        IAsyncCommandHandler<MarkTasksBugAsDone>,
        IAsyncCommandHandler<AssignAssigneeToTasksBug>,
        IAsyncCommandHandler<AssignTasksBugToSprint>,
        IAsyncCommandHandler<AssignLabelsToSubtask>,
        IAsyncCommandHandler<CommentSubtask>,
        IAsyncCommandHandler<MarkSubtaskAsInProgress>,
        IAsyncCommandHandler<MarkSubtaskAsDone>,
        IAsyncCommandHandler<AssignAssigneeToSubtask>,
        IAsyncCommandHandler<AssignSubtaskToSprint>,
        IAsyncCommandHandler<AddSubtaskToTask>
    {
        private readonly TaskRepository taskRepository;
        private readonly IIssueFactory taskFactory;
        private readonly ProjectRepository projectRepository;
        private readonly ILabelsSearcher labelsSearcher;
        private readonly IAuthorizationService authorizationService;
        private readonly UserRepository userRepository;
        private readonly ISprintSearcher sprintSearcher;

        public TaskCommandHandler(TaskRepository taskRepository, IIssueFactory taskFactory, ProjectRepository projectRepository, ILabelsSearcher labelsSearcher, IAuthorizationService authorizationService, UserRepository userRepository, ISprintSearcher sprintSearcher)
        {
            this.taskRepository = taskRepository;
            this.taskFactory = taskFactory;
            this.projectRepository = projectRepository;
            this.labelsSearcher = labelsSearcher;
            this.authorizationService = authorizationService;
            this.userRepository = userRepository;
            this.sprintSearcher = sprintSearcher;
        }

        public async Task HandleAsync(CreateTask command)
        {
            var task = await taskFactory.GenerateTask(command);
            await taskRepository.AddAsync(task);
        }

        public async Task HandleAsync(AssignLabelsToTask command)
        {
            var task = await taskRepository.GetAsync(command.IssueId);
            var labels = await labelsSearcher.GetLabels(task.ProjectId, command.LabelsIds);
            task.AssignLabels(command.LabelsIds, labels);
            await taskRepository.Update(task, task.Version);
        }

        public async Task HandleAsync(CommentTask command)
        {
            var task = await taskRepository.GetAsync(command.IssueId);
            await task.Comment(command.MemberId, command.Content, authorizationService);
            await taskRepository.Update(task, task.Version);
        }

        public async Task HandleAsync(MarkTaskAsInProgress command)
        {
            var task = await taskRepository.GetAsync(command.IssueId);
            var originalVersion = task.Version;
            task.MarkAsInProgress();
            await taskRepository.Update(task, originalVersion);
        }

        public async Task HandleAsync(MarkTaskAsDone command)
        {
            var task = await taskRepository.GetAsync(command.IssueId);
            var originalVersion = task.Version;
            task.MarkAsDone();
            await taskRepository.Update(task, originalVersion);
        }

        public async Task HandleAsync(AssignAssigneeToTask command)
        {
            var task = await taskRepository.GetAsync(command.IssueId);
            var originalVersion = task.Version;
            var assignee = await userRepository.GetAsync(command.UserId);
            await task.AssignAssignee(assignee, authorizationService);
            await taskRepository.Update(task, originalVersion);
        }

        public async Task HandleAsync(AssignTaskToSprint command)
        {
            var task = await taskRepository.GetAsync(command.IssueId);
            var originalVersion = task.Version;
            await task.AssignToSprint(command.SprintId, sprintSearcher);
            await taskRepository.Update(task, originalVersion);
        }
#region BUG
        public async Task HandleAsync(AddBugToTask command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            task.AddBug(taskFactory, command);
            var bug = task.Bugs.OrderBy(x => x.CreatedAt).Last();
            await taskRepository.UpdateChildEntity(task, originalVersion, bug);
        }

        public async Task HandleAsync(AssignLabelsToTasksBug command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            var labels = await labelsSearcher.GetLabels(task.ProjectId);
            task.AssignLabelsToBug(command.IssueId, command.LabelsIds, labels);
            var bug = task.Bugs.Single(x => x.Id == command.IssueId);
            await taskRepository.UpdateChildEntity(task, originalVersion, bug);
        }

        public async Task HandleAsync(CommentTasksBug command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            await task.CommentBug(command.IssueId, command.MemberId, command.Content, authorizationService);
            var bug = task.Bugs.Single(x => x.Id == command.IssueId);
            await taskRepository.UpdateChildEntity(task, originalVersion, bug);
        }

        public async Task HandleAsync(MarkTasksBugAsInProgress command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            task.MarkBugAsInProgress(command.IssueId);
            var bug = task.Bugs.Single(x => x.Id == command.IssueId);
            await taskRepository.UpdateChildEntity(task, originalVersion, bug);
        }

        public async Task HandleAsync(MarkTasksBugAsDone command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            task.MarkBugAsDone(command.IssueId);
            var bug = task.Bugs.Single(x => x.Id == command.IssueId);
            await taskRepository.UpdateChildEntity(task, originalVersion, bug);
        }

        public async Task HandleAsync(AssignAssigneeToTasksBug command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            var assignee = await userRepository.GetAsync(command.UserId);
            await task.AssignAssigneeToBug(command.IssueId, assignee, authorizationService);
            var bug = task.Bugs.Single(x => x.Id == command.IssueId);
            await taskRepository.UpdateChildEntity(task, originalVersion, bug);
        }

        public async Task HandleAsync(AssignTasksBugToSprint command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            await task.AssignBugToSprint(command.IssueId, command.SprintId, sprintSearcher);
            var bug = task.Bugs.Single(x => x.Id == command.IssueId);
            await taskRepository.UpdateChildEntity(task, originalVersion, bug);
        }
        #endregion

#region SUBTASK
        public async Task HandleAsync(AddSubtaskToTask command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            task.AddSubtask(taskFactory, command);
            var Subtask = task.Subtasks.OrderBy(x => x.CreatedAt).Last();
            await taskRepository.UpdateChildEntity(task, originalVersion, Subtask);
        }

        public async Task HandleAsync(AssignLabelsToSubtask command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            var labels = await labelsSearcher.GetLabels(task.ProjectId);
            task.AssignLabelsToSubtask(command.IssueId, command.LabelsIds, labels);
            var Subtask = task.Subtasks.Single(x => x.Id == command.IssueId);
            await taskRepository.UpdateChildEntity(task, originalVersion, Subtask);
        }

        public async Task HandleAsync(CommentSubtask command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            await task.CommentSubtask(command.IssueId, command.MemberId, command.Content, authorizationService);
            var Subtask = task.Subtasks.Single(x => x.Id == command.IssueId);
            await taskRepository.UpdateChildEntity(task, originalVersion, Subtask);
        }

        public async Task HandleAsync(MarkSubtaskAsInProgress command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            task.MarkSubtaskAsInProgress(command.IssueId);
            var Subtask = task.Subtasks.Single(x => x.Id == command.IssueId);
            await taskRepository.UpdateChildEntity(task, originalVersion, Subtask);
        }

        public async Task HandleAsync(MarkSubtaskAsDone command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            task.MarkSubtaskAsDone(command.IssueId);
            var Subtask = task.Subtasks.Single(x => x.Id == command.IssueId);
            await taskRepository.UpdateChildEntity(task, originalVersion, Subtask);
        }

        public async Task HandleAsync(AssignAssigneeToSubtask command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            var assignee = await userRepository.GetAsync(command.UserId);
            await task.AssignAssigneeToSubtask(command.IssueId, assignee, authorizationService);
            var Subtask = task.Subtasks.Single(x => x.Id == command.IssueId);
            await taskRepository.UpdateChildEntity(task, originalVersion, Subtask);
        }

        public async Task HandleAsync(AssignSubtaskToSprint command)
        {
            var task = await taskRepository.GetAsync(command.TaskId);
            var originalVersion = task.Version;
            await task.AssignSubtaskToSprint(command.IssueId, command.SprintId, sprintSearcher);
            var Subtask = task.Subtasks.Single(x => x.Id == command.IssueId);
            await taskRepository.UpdateChildEntity(task, originalVersion, Subtask);
        }
#endregion
    }
}

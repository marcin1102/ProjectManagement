using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Infrastructure.Storage.EF.Repository;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Contracts.Bug.Events;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Subtask.Events;
using ProjectManagement.Contracts.Task.Events;
using ProjectManagementView.Storage.Searchers;

namespace ProjectManagementView.Storage.Handlers
{
    public class TaskEventHandler :
        IAsyncEventHandler<TaskCreated>,
        IAsyncEventHandler<TaskAssignedToSprint>,
        IAsyncEventHandler<AssigneeAssignedToTask>,
        IAsyncEventHandler<LabelAssignedToTask>,
        IAsyncEventHandler<TaskCommented>,
        IAsyncEventHandler<TaskMarkedAsInProgress>,
        IAsyncEventHandler<TaskMarkedAsDone>,
        IAsyncEventHandler<BugAddedToTask>,
        IAsyncEventHandler<SubtaskAddedToTask>,
        IAsyncEventHandler<LabelAssignedToSubtask>,
        IAsyncEventHandler<SubtaskAssignedToSprint>,
        IAsyncEventHandler<SubtaskCommented>,
        IAsyncEventHandler<SubtaskMarkedAsInProgress>,
        IAsyncEventHandler<SubtaskMarkedAsDone>
    {
        private readonly IRepository<Models.Project> projectRepository;
        private readonly IRepository<Models.Task> taskRepository;
        private readonly IRepository<Models.User> userRepository;
        private readonly IRepository<Models.Sprint> sprintRepository;
        private readonly ILabelSearcher labelSearcher;
        private readonly IRepository<Models.Subtask> subtaskRepository;

        public TaskEventHandler(IRepository<Models.Project> projectRepository, IRepository<Models.Task> taskRepository, IRepository<Models.User> userRepository, IRepository<Models.Sprint> sprintRepository, ILabelSearcher labelSearcher, IRepository<Models.Subtask> subtaskRepository)
        {
            this.projectRepository = projectRepository;
            this.taskRepository = taskRepository;
            this.userRepository = userRepository;
            this.sprintRepository = sprintRepository;
            this.labelSearcher = labelSearcher;
            this.subtaskRepository = subtaskRepository;
        }

        public async Task HandleAsync(TaskCreated @event)
        {
            var project = await projectRepository.GetAsync(@event.ProjectId);
            var reporter = await userRepository.GetAsync(@event.ReporterId);

            var labels = await labelSearcher.GetLabels(@event.LabelsIds.ToList());
            project.Tasks.Add(new Models.Task(@event.Id)
            {
                ProjectId = @event.ProjectId,
                Title = @event.Title,
                Description = @event.Description,
                Status = IssueStatus.Todo,
                Reporter = reporter,
                Assignee = @event.AssigneeId != null ? await userRepository.GetAsync(@event.AssigneeId.Value) : null,
                CreatedAt = @event.CreatedAt,
                Labels = labels
            });

            await projectRepository.Update(project);
        }

        public async Task HandleAsync(TaskAssignedToSprint @event)
        {
            var sprint = await sprintRepository.GetAsync(@event.SprintId);
            var task = await taskRepository.GetAsync(@event.IssueId);
            sprint.Tasks.Add(task);
            task.Version = @event.AggregateVersion;
            await sprintRepository.Update(sprint);
        }

        public async Task HandleAsync(AssigneeAssignedToTask @event)
        {
            var task = await taskRepository.GetAsync(@event.IssueId);
            var assignee = await userRepository.GetAsync(@event.AssigneeId);
            task.Assignee = assignee;
            task.Version = @event.AggregateVersion;
            await taskRepository.Update(task);
        }

        public async Task HandleAsync(LabelAssignedToTask @event)
        {
            var task = await taskRepository.GetAsync(@event.IssueId);
            var labels = await labelSearcher.GetLabels(@event.LabelsIds.ToList());
            task.Labels = labels;
            task.Version = @event.AggregateVersion;
            await taskRepository.Update(task);
        }

        public async Task HandleAsync(TaskCommented @event)
        {
            var task = await taskRepository.GetAsync(@event.IssueId);
            var comments = task.Comments;
            comments.Add(new Models.Comment(@event.CommentId, @event.Content, @event.AddedAt));
            task.Comments = comments;
            task.Version = @event.AggregateVersion;
            await taskRepository.Update(task);
        }

        public async Task HandleAsync(TaskMarkedAsInProgress @event)
        {
            var task = await taskRepository.GetAsync(@event.Id);
            task.Status = @event.Status;
            task.Version = @event.AggregateVersion;
            await taskRepository.Update(task);
        }

        public async Task HandleAsync(TaskMarkedAsDone @event)
        {
            var task = await taskRepository.GetAsync(@event.Id);
            task.Status = @event.Status;
            task.Version = @event.AggregateVersion;
            await taskRepository.Update(task);
        }

        public async Task HandleAsync(BugAddedToTask @event)
        {
            var task = await taskRepository.GetAsync(@event.TaskId);
            var project = await projectRepository.GetAsync(task.ProjectId);
            var bug = new Models.Bug(@event.BugId)
            {
                ProjectId = @event.ProjectId,
                Title = @event.Title,
                Description = @event.Description,
                CreatedAt = @event.CreatedAt,
                Status = IssueStatus.Todo,
                Labels = await labelSearcher.GetLabels(@event.LabelsIds.ToList()),
                Reporter = await userRepository.GetAsync(@event.ReporterId),
                Assignee = @event.AssigneeId.HasValue ? await userRepository.GetAsync(@event.AssigneeId.Value) : null
            };

            task.Bugs.Add(bug);
            project.Bugs.Add(bug);

            await taskRepository.Update(task);
            await projectRepository.Update(project);
        }

        public async Task HandleAsync(SubtaskAddedToTask @event)
        {
            var task = await taskRepository.GetAsync(@event.TaskId);
            var project = await projectRepository.GetAsync(task.ProjectId);
            var subtask = new Models.Subtask(@event.SubtaskId)
            {
                ProjectId = @event.ProjectId,
                Title = @event.Title,
                Description = @event.Description,
                CreatedAt = @event.CreatedAt,
                Status = IssueStatus.Todo,
                Labels = await labelSearcher.GetLabels(@event.LabelsIds.ToList()),
                Reporter = await userRepository.GetAsync(@event.ReporterId),
                Assignee = @event.AssigneeId.HasValue ? await userRepository.GetAsync(@event.AssigneeId.Value) : null
            };

            task.Subtasks.Add(subtask);
            project.Subtasks.Add(subtask);

            await taskRepository.Update(task);
            await projectRepository.Update(project);
        }

        public async Task HandleAsync(LabelAssignedToSubtask @event)
        {
            var subtask = await subtaskRepository.GetAsync(@event.IssueId);
            var labels = await labelSearcher.GetLabels(@event.LabelsIds.ToList());
            subtask.Labels = labels;
            await subtaskRepository.Update(subtask);
        }

        public async Task HandleAsync(SubtaskAssignedToSprint @event)
        {
            var sprint = await sprintRepository.GetAsync(@event.SprintId);
            var subtask = await subtaskRepository.GetAsync(@event.IssueId);
            sprint.Subtasks.Add(subtask);
            await sprintRepository.Update(sprint);
        }

        public async Task HandleAsync(SubtaskCommented @event)
        {
            var subtask = await subtaskRepository.GetAsync(@event.IssueId);
            var comments = subtask.Comments;
            comments.Add(new Models.Comment(@event.CommentId, @event.Content, @event.AddedAt));
            subtask.Comments = comments;
            subtask.Version = @event.AggregateVersion;
            await subtaskRepository.Update(subtask);
        }

        public async Task HandleAsync(SubtaskMarkedAsInProgress @event)
        {
            var subtask = await subtaskRepository.GetAsync(@event.Id);
            subtask.Status = @event.Status;
            await subtaskRepository.Update(subtask);
        }

        public async Task HandleAsync(SubtaskMarkedAsDone @event)
        {
            var subtask = await subtaskRepository.GetAsync(@event.Id);
            subtask.Status = @event.Status;
            await subtaskRepository.Update(subtask);
        }
    }
}

using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Infrastructure.Storage.EF.Repository;
using ProjectManagement.Contracts.Bug.Events;
using ProjectManagement.Contracts.Issue.Enums;
using ProjectManagement.Contracts.Task.Commands;
using ProjectManagementView.Storage.Searchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagementView.Storage.Handlers
{
    public class BugEventHandler :
        IAsyncEventHandler<BugCreated>,
        IAsyncEventHandler<BugAssignedToSprint>,
        IAsyncEventHandler<AssigneeAssignedToBug>,
        IAsyncEventHandler<LabelAssignedToBug>,
        IAsyncEventHandler<BugCommented>,
        IAsyncEventHandler<BugMarkedAsInProgress>,
        IAsyncEventHandler<BugMarkedAsDone>
        //IAsyncEventHandler<ChildBugChangedToStandAloneBug>
    {
        private readonly IRepository<Models.Project> projectRepository;
        private readonly IRepository<Models.Bug> bugRepository;
        private readonly IRepository<Models.User> userRepository;
        private readonly IRepository<Models.Sprint> sprintRepository;
        private readonly IRepository<Models.Task> taskRepository;
        private readonly IRepository<Models.Nfr> nfrRepository;
        private readonly ILabelSearcher labelSearcher;
        private readonly ProjectManagementViewContext db;

        public BugEventHandler(IRepository<Models.Project> projectRepository, IRepository<Models.Bug> bugRepository, IRepository<Models.User> userRepository,
            IRepository<Models.Sprint> sprintRepository, IRepository<Models.Task> taskRepository, IRepository<Models.Nfr> nfrRepository, ILabelSearcher labelSearcher, ProjectManagementViewContext db)
        {
            this.projectRepository = projectRepository;
            this.bugRepository = bugRepository;
            this.userRepository = userRepository;
            this.sprintRepository = sprintRepository;
            this.taskRepository = taskRepository;
            this.nfrRepository = nfrRepository;
            this.labelSearcher = labelSearcher;
            this.db = db;
        }

        public async Task HandleAsync(BugCreated @event)
        {
            var project = await projectRepository.GetAsync(@event.ProjectId);
            var reporter = await userRepository.GetAsync(@event.ReporterId);

            var labels = await labelSearcher.GetLabels(@event.LabelsIds.ToList());
            project.Bugs.Add(new Models.Bug(@event.Id)
            {
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

        public async Task HandleAsync(BugAssignedToSprint @event)
        {
            var sprint = await sprintRepository.GetAsync(@event.SprintId);
            var bug = await bugRepository.GetAsync(@event.IssueId);
            var sqlQuery = $"UPDATE \"project-management-views\".\"Issues\" SET \"SprintId\" = '{@event.SprintId}', \"Version\" = '{@event.AggregateVersion}' WHERE \"project-management-views\".\"Issues\".\"Id\" = '{bug.Id}'";
            await db.Database.ExecuteSqlCommandAsync(sqlQuery);
        }

        public async Task HandleAsync(AssigneeAssignedToBug @event)
        {
            var Bug = await bugRepository.GetAsync(@event.IssueId);
            var assignee = await userRepository.GetAsync(@event.AssigneeId);
            Bug.Assignee = assignee;
            await bugRepository.Update(Bug);
        }

        public async Task HandleAsync(LabelAssignedToBug @event)
        {
            var Bug = await bugRepository.GetAsync(@event.IssueId);
            var labels = await labelSearcher.GetLabels(@event.LabelsIds.ToList());
            Bug.Labels = labels;
            await bugRepository.Update(Bug);
        }

        public async Task HandleAsync(BugCommented @event)
        {
            var Bug = await bugRepository.GetAsync(@event.IssueId);
            var comments = Bug.Comments;
            comments.Add(new Models.Comment(@event.CommentId, @event.MemberId, @event.Content, @event.AddedAt));
            Bug.Comments = comments;
            await bugRepository.Update(Bug);
        }

        public async Task HandleAsync(BugMarkedAsInProgress @event)
        {
            var Bug = await bugRepository.GetAsync(@event.Id);
            Bug.Status = @event.Status;
            await bugRepository.Update(Bug);
        }

        public async Task HandleAsync(BugMarkedAsDone @event)
        {
            var Bug = await bugRepository.GetAsync(@event.Id);
            Bug.Status = @event.Status;
            await bugRepository.Update(Bug);
        }

        //public async Task HandleAsync(ChangeTasksBugToBug @event)
        //{
        //    var task = await taskRepository.GetAsync(@event.TaskId);
        //    var bug = task.Bugs.SingleOrDefault(x => x.Id == @event.ChildBugId);
        //    if(bug == null)
        //        throw
        //}
    }
}

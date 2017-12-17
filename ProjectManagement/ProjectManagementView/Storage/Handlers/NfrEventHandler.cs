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
using ProjectManagement.Contracts.Nfr.Events;
using ProjectManagementView.Storage.Searchers;

namespace ProjectManagementView.Storage.Handlers
{
    public class NfrEventHandler :
        IAsyncEventHandler<NfrCreated>,
        IAsyncEventHandler<NfrAssignedToSprint>,
        IAsyncEventHandler<AssigneeAssignedToNfr>,
        IAsyncEventHandler<LabelAssignedToNfr>,
        IAsyncEventHandler<NfrCommented>,
        IAsyncEventHandler<NfrMarkedAsInProgress>,
        IAsyncEventHandler<NfrMarkedAsDone>,
        IAsyncEventHandler<BugAddedToNfr>
    {
        private readonly IRepository<Models.Project> projectRepository;
        private readonly IRepository<Models.Nfr> nfrRepository;
        private readonly IRepository<Models.User> userRepository;
        private readonly IRepository<Models.Sprint> sprintRepository;
        private readonly ILabelSearcher labelSearcher;
        private readonly IRepository<Models.Bug> bugRepository;

        public NfrEventHandler(IRepository<Models.Project> projectRepository, IRepository<Models.Nfr> nfrRepository, IRepository<Models.User> userRepository, IRepository<Models.Sprint> sprintRepository, ILabelSearcher labelSearcher, IRepository<Models.Bug> bugRepository)
        {
            this.projectRepository = projectRepository;
            this.nfrRepository = nfrRepository;
            this.userRepository = userRepository;
            this.sprintRepository = sprintRepository;
            this.labelSearcher = labelSearcher;
            this.bugRepository = bugRepository;
        }

        public async Task HandleAsync(NfrCreated @event)
        {
            var project = await projectRepository.GetAsync(@event.ProjectId);
            var reporter = await userRepository.GetAsync(@event.ReporterId);

            var labels = await labelSearcher.GetLabels(@event.LabelsIds.ToList());
            project.Nfrs.Add(new Models.Nfr(@event.Id)
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

        public async Task HandleAsync(NfrAssignedToSprint @event)
        {
            var sprint = await sprintRepository.GetAsync(@event.SprintId);
            var Nfr = await nfrRepository.GetAsync(@event.IssueId);
            sprint.Nfrs.Add(Nfr);
            Nfr.Version = @event.AggregateVersion;
            await sprintRepository.Update(sprint);
        }

        public async Task HandleAsync(AssigneeAssignedToNfr @event)
        {
            var Nfr = await nfrRepository.GetAsync(@event.IssueId);
            var assignee = await userRepository.GetAsync(@event.AssigneeId);
            Nfr.Assignee = assignee;
            Nfr.Version = @event.AggregateVersion;
            await nfrRepository.Update(Nfr);
        }

        public async Task HandleAsync(LabelAssignedToNfr @event)
        {
            var Nfr = await nfrRepository.GetAsync(@event.IssueId);
            var labels = await labelSearcher.GetLabels(@event.LabelsIds.ToList());
            Nfr.Labels = labels;
            Nfr.Version = @event.AggregateVersion;
            await nfrRepository.Update(Nfr);
        }

        public async Task HandleAsync(NfrCommented @event)
        {
            var Nfr = await nfrRepository.GetAsync(@event.IssueId);
            var comments = Nfr.Comments;
            comments.Add(new Models.Comment(@event.CommentId, @event.Content, @event.AddedAt));
            Nfr.Comments = comments;
            Nfr.Version = @event.AggregateVersion;
            await nfrRepository.Update(Nfr);
        }

        public async Task HandleAsync(NfrMarkedAsInProgress @event)
        {
            var Nfr = await nfrRepository.GetAsync(@event.Id);
            Nfr.Status = @event.Status;
            Nfr.Version = @event.AggregateVersion;
            await nfrRepository.Update(Nfr);
        }

        public async Task HandleAsync(NfrMarkedAsDone @event)
        {
            var Nfr = await nfrRepository.GetAsync(@event.Id);
            Nfr.Status = @event.Status;
            Nfr.Version = @event.AggregateVersion;
            await nfrRepository.Update(Nfr);
        }

        public async Task HandleAsync(BugAddedToNfr @event)
        {
            var nfr = await nfrRepository.GetAsync(@event.NfrId);
            var project = await projectRepository.GetAsync(nfr.ProjectId);
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
            nfr.Bugs.Add(bug);
            project.Bugs.Add(bug);

            await nfrRepository.Update(nfr);
            await projectRepository.Update(project);
        }
    }
}

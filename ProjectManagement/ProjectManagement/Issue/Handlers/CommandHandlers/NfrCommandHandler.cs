using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Nfr.Commands;
using ProjectManagement.Issue.Factory;
using ProjectManagement.Issue.Repository;
using ProjectManagement.Label.Searcher;
using ProjectManagement.Project.Repository;
using ProjectManagement.Services;
using ProjectManagement.Sprint.Searchers;
using ProjectManagement.User.Repository;

namespace ProjectManagement.Issue.Handlers.CommandHandlers
{
    public class NfrCommandHandler :
        IAsyncCommandHandler<CreateNfr>,
        IAsyncCommandHandler<AssignLabelsToNfr>,
        IAsyncCommandHandler<CommentNfr>,
        IAsyncCommandHandler<MarkNfrAsInProgress>,
        IAsyncCommandHandler<MarkNfrAsDone>,
        IAsyncCommandHandler<AssignAssigneeToNfr>,
        IAsyncCommandHandler<AssignNfrToSprint>,
        IAsyncCommandHandler<AddBugToNfr>,
        IAsyncCommandHandler<AssignLabelsToNfrsBug>,
        IAsyncCommandHandler<CommentNfrsBug>,
        IAsyncCommandHandler<MarkNfrsBugAsInProgress>,
        IAsyncCommandHandler<MarkNfrsBugAsDone>,
        IAsyncCommandHandler<AssignAssigneeToNfrsBug>,
        IAsyncCommandHandler<AssignNfrsBugToSprint>
    {
        private readonly NfrRepository nfrRepository;
        private readonly IIssueFactory issueFactory;
        private readonly ProjectRepository projectRepository;
        private readonly ILabelsSearcher labelsSearcher;
        private readonly IAuthorizationService authorizationService;
        private readonly UserRepository userRepository;
        private readonly ISprintSearcher sprintSearcher;

        public NfrCommandHandler(NfrRepository nfrRepository, IIssueFactory issueFactory, ProjectRepository projectRepository, ILabelsSearcher labelsSearcher, IAuthorizationService authorizationService, UserRepository userRepository, ISprintSearcher sprintSearcher)
        {
            this.nfrRepository = nfrRepository;
            this.issueFactory = issueFactory;
            this.projectRepository = projectRepository;
            this.labelsSearcher = labelsSearcher;
            this.authorizationService = authorizationService;
            this.userRepository = userRepository;
            this.sprintSearcher = sprintSearcher;
        }

        public async Task HandleAsync(CreateNfr command)
        {
            if (await projectRepository.FindAsync(command.ProjectId) == null)
                throw new EntityDoesNotExist(command.ProjectId, nameof(Project.Model.Project));

            var issue = await issueFactory.GenerateNfr(command);

            await nfrRepository.AddAsync(issue);
            command.CreatedId = issue.Id;
        }

        public async Task HandleAsync(AssignLabelsToNfr command)
        {
            var issue = await nfrRepository.GetAsync(command.IssueId);
            var labels = await labelsSearcher.GetLabels(issue.ProjectId, command.LabelsIds);
            issue.AssignLabels(command.LabelsIds, labels);
            await nfrRepository.Update(issue, issue.Version);
        }

        public async Task HandleAsync(CommentNfr command)
        {
            var issue = await nfrRepository.GetAsync(command.IssueId);
            issue.Comment(command.MemberId, command.Content, authorizationService);
            await nfrRepository.Update(issue, issue.Version);
        }

        public async Task HandleAsync(MarkNfrAsInProgress command)
        {
            var issue = await nfrRepository.GetAsync(command.IssueId);
            var originalVersion = issue.Version;
            issue.MarkAsInProgress();
            await nfrRepository.Update(issue, originalVersion);
        }

        public async Task HandleAsync(MarkNfrAsDone command)
        {
            var Nfr = await nfrRepository.GetAsync(command.IssueId);
            var originalVersion = Nfr.Version;

            Nfr.MarkAsDone();
            await nfrRepository.Update(Nfr, originalVersion);
        }

        public async Task HandleAsync(AssignAssigneeToNfr command)
        {
            var issue = await nfrRepository.GetAsync(command.IssueId);
            var originalVersion = issue.Version;
            var assignee = await userRepository.GetAsync(command.UserId);
            issue.AssignAssignee(assignee, authorizationService);
            await nfrRepository.Update(issue, originalVersion);
        }

        public async Task HandleAsync(AssignNfrToSprint command)
        {
            var issue = await nfrRepository.GetAsync(command.IssueId);
            var originalVersion = issue.Version;
            issue.AssignToSprint(command.SprintId, sprintSearcher);
            await nfrRepository.Update(issue, originalVersion);
        }

        #region BUG
        public async Task HandleAsync(AddBugToNfr command)
        {
            var nfr = await nfrRepository.GetWithBugsAsync(command.NfrId);
            var originalVersion = nfr.Version;
            nfr.AddBug(issueFactory, command);
            var bug = nfr.Bugs.OrderBy(x => x.CreatedAt).Last();
            await nfrRepository.UpdateChildEntity(nfr, originalVersion, bug);
        }

        public async Task HandleAsync(AssignLabelsToNfrsBug command)
        {
            var task = await nfrRepository.GetWithBugsAndLabelsAsync(command.NfrId);
            var originalVersion = task.Version;
            var labels = await labelsSearcher.GetLabels(task.ProjectId);
            task.AssignLabelsToBug(command.IssueId, command.LabelsIds, labels);
            var bug = task.Bugs.Single(x => x.Id == command.IssueId);
            await nfrRepository.UpdateChildEntity(task, originalVersion, bug);
        }

        public async Task HandleAsync(CommentNfrsBug command)
        {
            var task = await nfrRepository.GetWithBugsAndCommentsAsync(command.NfrId);
            var originalVersion = task.Version;
            task.CommentBug(command.IssueId, command.MemberId, command.Content, authorizationService);
            var bug = task.Bugs.Single(x => x.Id == command.IssueId);
            await nfrRepository.UpdateChildEntity(task, originalVersion, bug);
        }

        public async Task HandleAsync(MarkNfrsBugAsInProgress command)
        {
            var task = await nfrRepository.GetWithBugsAsync(command.NfrId);
            var originalVersion = task.Version;
            task.MarkBugAsInProgress(command.IssueId);
            var bug = task.Bugs.Single(x => x.Id == command.IssueId);
            await nfrRepository.UpdateChildEntity(task, originalVersion, bug);
        }

        public async Task HandleAsync(MarkNfrsBugAsDone command)
        {
            var task = await nfrRepository.GetWithBugsAsync(command.NfrId);
            var originalVersion = task.Version;
            task.MarkBugAsDone(command.IssueId);
            var bug = task.Bugs.Single(x => x.Id == command.IssueId);
            await nfrRepository.UpdateChildEntity(task, originalVersion, bug);
        }

        public async Task HandleAsync(AssignAssigneeToNfrsBug command)
        {
            var task = await nfrRepository.GetWithBugsAsync(command.NfrId);
            var originalVersion = task.Version;
            var assignee = await userRepository.GetAsync(command.UserId);
            task.AssignAssigneeToBug(command.IssueId, assignee, authorizationService);
            var bug = task.Bugs.Single(x => x.Id == command.IssueId);
            await nfrRepository.UpdateChildEntity(task, originalVersion, bug);
        }

        public async Task HandleAsync(AssignNfrsBugToSprint command)
        {
            var task = await nfrRepository.GetWithBugsAsync(command.NfrId);
            var originalVersion = task.Version;
            task.AssignBugToSprint(command.IssueId, command.SprintId, sprintSearcher);
            var bug = task.Bugs.Single(x => x.Id == command.IssueId);
            await nfrRepository.UpdateChildEntity(task, originalVersion, bug);
        }
        #endregion
    }
}

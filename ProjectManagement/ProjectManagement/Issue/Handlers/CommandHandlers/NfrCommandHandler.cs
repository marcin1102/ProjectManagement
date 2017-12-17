using System.Linq;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Nfr.Commands;
using ProjectManagement.Issue.Factory;
using ProjectManagement.Issue.Repository;
using ProjectManagement.Label.Searcher;
using ProjectManagement.Project.Repository;
using ProjectManagement.Services;
using ProjectManagement.Sprint.Searchers;
using ProjectManagement.User.Repository;
using ProjectManagement.Infrastructure.CallContexts;

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
        private readonly CallContext callContext;

        public NfrCommandHandler(NfrRepository nfrRepository, IIssueFactory issueFactory, ProjectRepository projectRepository, ILabelsSearcher labelsSearcher, IAuthorizationService authorizationService, UserRepository userRepository, ISprintSearcher sprintSearcher, CallContext callContext)
        {
            this.nfrRepository = nfrRepository;
            this.issueFactory = issueFactory;
            this.projectRepository = projectRepository;
            this.labelsSearcher = labelsSearcher;
            this.authorizationService = authorizationService;
            this.userRepository = userRepository;
            this.sprintSearcher = sprintSearcher;
            this.callContext = callContext;
        }

        public async Task HandleAsync(CreateNfr command)
        {            
            var issue = await issueFactory.GenerateNfr(command);
            await nfrRepository.AddAsync(issue);
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
            await issue.Comment(callContext.UserId, command.Content, authorizationService);
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
            await issue.AssignAssignee(assignee, authorizationService);
            await nfrRepository.Update(issue, originalVersion);
        }

        public async Task HandleAsync(AssignNfrToSprint command)
        {
            var issue = await nfrRepository.GetAsync(command.IssueId);
            var originalVersion = issue.Version;
            await issue.AssignToSprint(command.SprintId, sprintSearcher);
            await nfrRepository.Update(issue, originalVersion);
        }

        #region BUG
        public async Task HandleAsync(AddBugToNfr command)
        {
            var nfr = await nfrRepository.GetAsync(command.NfrId);
            var originalVersion = nfr.Version;
            await nfr.AddBug(issueFactory, command);
            var bug = nfr.Bugs.OrderBy(x => x.CreatedAt).Last();
            await nfrRepository.UpdateChildEntity(nfr, originalVersion, bug);
        }

        public async Task HandleAsync(AssignLabelsToNfrsBug command)
        {
            var nfr = await nfrRepository.GetAsync(command.NfrId);
            var originalVersion = nfr.Version;
            var labels = await labelsSearcher.GetLabels(nfr.ProjectId);
            nfr.AssignLabelsToBug(command.IssueId, command.LabelsIds, labels);
            var bug = nfr.Bugs.Single(x => x.Id == command.IssueId);
            await nfrRepository.UpdateChildEntity(nfr, originalVersion, bug);
        }

        public async Task HandleAsync(CommentNfrsBug command)
        {
            var nfr = await nfrRepository.GetAsync(command.NfrId);
            var originalVersion = nfr.Version;
            await nfr.CommentBug(command.IssueId, callContext.UserId, command.Content, authorizationService);
            var bug = nfr.Bugs.Single(x => x.Id == command.IssueId);
            await nfrRepository.UpdateChildEntity(nfr, originalVersion, bug);
        }

        public async Task HandleAsync(MarkNfrsBugAsInProgress command)
        {
            var nfr = await nfrRepository.GetAsync(command.NfrId);
            var originalVersion = nfr.Version;
            nfr.MarkBugAsInProgress(command.IssueId);
            var bug = nfr.Bugs.Single(x => x.Id == command.IssueId);
            await nfrRepository.UpdateChildEntity(nfr, originalVersion, bug);
        }

        public async Task HandleAsync(MarkNfrsBugAsDone command)
        {
            var nfr = await nfrRepository.GetAsync(command.NfrId);
            var originalVersion = nfr.Version;
            nfr.MarkBugAsDone(command.IssueId);
            var bug = nfr.Bugs.Single(x => x.Id == command.IssueId);
            await nfrRepository.UpdateChildEntity(nfr, originalVersion, bug);
        }

        public async Task HandleAsync(AssignAssigneeToNfrsBug command)
        {
            var nfr = await nfrRepository.GetAsync(command.NfrId);
            var originalVersion = nfr.Version;
            var assignee = await userRepository.GetAsync(command.UserId);
            await nfr.AssignAssigneeToBug(command.IssueId, assignee, authorizationService);
            var bug = nfr.Bugs.Single(x => x.Id == command.IssueId);
            await nfrRepository.UpdateChildEntity(nfr, originalVersion, bug);
        }

        public async Task HandleAsync(AssignNfrsBugToSprint command)
        {
            var nfr = await nfrRepository.GetAsync(command.NfrId);
            var originalVersion = nfr.Version;
            await nfr.AssignBugToSprint(command.IssueId, command.SprintId, sprintSearcher);
            var bug = nfr.Bugs.Single(x => x.Id == command.IssueId);
            await nfrRepository.UpdateChildEntity(nfr, originalVersion, bug);
        }
        #endregion
    }
}

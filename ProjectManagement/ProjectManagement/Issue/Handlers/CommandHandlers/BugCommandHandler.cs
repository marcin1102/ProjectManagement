using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Bug.Commands;
using ProjectManagement.Issue.Repository;
using ProjectManagement.Issue.Factory;
using ProjectManagement.Label.Searcher;
using ProjectManagement.Services;
using ProjectManagement.User.Repository;
using ProjectManagement.Sprint.Searchers;
using ProjectManagement.Infrastructure.CallContexts;

namespace ProjectManagement.Issue.Handlers.CommandHandlers
{
    public class BugCommandHandler :
        IAsyncCommandHandler<CreateBug>,
        IAsyncCommandHandler<AssignLabelsToBug>,
        IAsyncCommandHandler<CommentBug>,
        IAsyncCommandHandler<MarkBugAsInProgress>,
        IAsyncCommandHandler<MarkBugAsDone>,
        IAsyncCommandHandler<AssignAssigneeToBug>,
        IAsyncCommandHandler<AssignBugToSprint>
    {
        private readonly BugRepository bugRepository;
        private readonly IIssueFactory issueFactory;
        private readonly ILabelsSearcher labelsSearcher;
        private readonly IMembershipService authorizationService;
        private readonly UserRepository userRepository;
        private readonly ISprintSearcher sprintSearcher;
        private readonly CallContext callContext;

        public BugCommandHandler(BugRepository bugRepository, IIssueFactory issueFactory, ILabelsSearcher labelsSearcher, IMembershipService authorizationService, UserRepository userRepository, ISprintSearcher sprintSearcher, CallContext callContext)
        {
            this.bugRepository = bugRepository;
            this.issueFactory = issueFactory;
            this.labelsSearcher = labelsSearcher;
            this.authorizationService = authorizationService;
            this.userRepository = userRepository;
            this.sprintSearcher = sprintSearcher;
            this.callContext = callContext;
        }

        public async Task HandleAsync(CreateBug command)
        {
            var Bug = await issueFactory.Create(command);
            await bugRepository.AddAsync((Model.Bug)Bug);
        }

        public async Task HandleAsync(AssignLabelsToBug command)
        {
            var Bug = await bugRepository.GetAsync(command.IssueId);
            var labels = await labelsSearcher.GetLabels(Bug.ProjectId, command.LabelsIds);
            Bug.AssignLabels(command.LabelsIds, labels);
            await bugRepository.Update(Bug, Bug.Version);
        }

        public async Task HandleAsync(CommentBug command)
        {
            var Bug = await bugRepository.GetAsync(command.IssueId);
            var version = Bug.Version;
            await Bug.Comment(callContext.UserId, command.Content, authorizationService);
            await bugRepository.Update(Bug, version);
        }

        public async Task HandleAsync(MarkBugAsInProgress command)
        {
            var Bug = await bugRepository.GetAsync(command.IssueId);
            var originalVersion = Bug.Version;
            await Bug.MarkAsInProgress(callContext.UserId, authorizationService);
            await bugRepository.Update(Bug, originalVersion);
        }

        public async Task HandleAsync(MarkBugAsDone command)
        {
            var Bug = await bugRepository.GetAsync(command.IssueId);
            var originalVersion = Bug.Version;
            await Bug.MarkAsDone(callContext.UserId, authorizationService);
            await bugRepository.Update(Bug, originalVersion);
        }

        public async Task HandleAsync(AssignAssigneeToBug command)
        {
            var Bug = await bugRepository.GetAsync(command.IssueId);
            var originalVersion = Bug.Version;
            var assignee = await userRepository.GetAsync(command.AssigneeId);
            await Bug.AssignAssignee(assignee, authorizationService);
            await bugRepository.Update(Bug, originalVersion);
        }

        public async Task HandleAsync(AssignBugToSprint command)
        {
            var Bug = await bugRepository.GetAsync(command.IssueId);
            var originalVersion = Bug.Version;
            await Bug.AssignToSprint(command.SprintId, sprintSearcher);
            await bugRepository.Update(Bug, originalVersion);
        }
    }
}
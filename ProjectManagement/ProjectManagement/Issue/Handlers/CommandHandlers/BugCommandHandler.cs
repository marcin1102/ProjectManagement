using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Bug.Commands;
using ProjectManagement.Issue.Repository;
using ProjectManagement.Issue.Factory;
using ProjectManagement.Label.Searcher;
using ProjectManagement.Services;
using ProjectManagement.User.Repository;
using ProjectManagement.Sprint.Searchers;

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
        private readonly IssueFactory issueFactory;
        private readonly ILabelsSearcher labelsSearcher;
        private readonly IAuthorizationService authorizationService;
        private readonly UserRepository userRepository;
        private readonly ISprintSearcher sprintSearcher;

        public BugCommandHandler(BugRepository bugRepository, IssueFactory issueFactory, ILabelsSearcher labelsSearcher, IAuthorizationService authorizationService, UserRepository userRepository, ISprintSearcher sprintSearcher)
        {
            this.bugRepository = bugRepository;
            this.issueFactory = issueFactory;
            this.labelsSearcher = labelsSearcher;
            this.authorizationService = authorizationService;
            this.userRepository = userRepository;
            this.sprintSearcher = sprintSearcher;
        }

        public async Task HandleAsync(CreateBug command)
        {
            var Bug = await issueFactory.GenerateBug(command);
            await bugRepository.AddAsync(Bug);
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
            await Bug.Comment(command.MemberId, command.Content, authorizationService);
            await bugRepository.Update(Bug, Bug.Version);
        }

        public async Task HandleAsync(MarkBugAsInProgress command)
        {
            var Bug = await bugRepository.GetAsync(command.IssueId);
            var originalVersion = Bug.Version;
            Bug.MarkAsInProgress();
            await bugRepository.Update(Bug, originalVersion);
        }

        public async Task HandleAsync(MarkBugAsDone command)
        {
            var Bug = await bugRepository.GetAsync(command.IssueId);
            var originalVersion = Bug.Version;
            Bug.MarkAsDone();
            await bugRepository.Update(Bug, originalVersion);
        }

        public async Task HandleAsync(AssignAssigneeToBug command)
        {
            var Bug = await bugRepository.GetAsync(command.IssueId);
            var originalVersion = Bug.Version;
            var assignee = await userRepository.GetAsync(command.UserId);
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
using System.Threading.Tasks;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Bug.Commands;

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
        public Task HandleAsync(CreateBug command)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleAsync(AssignLabelsToBug command)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleAsync(CommentBug command)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleAsync(MarkBugAsInProgress command)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleAsync(MarkBugAsDone command)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleAsync(AssignAssigneeToBug command)
        {
            throw new System.NotImplementedException();
        }

        public Task HandleAsync(AssignBugToSprint command)
        {
            throw new System.NotImplementedException();
        }
    }
}
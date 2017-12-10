using Infrastructure.CallContexts;
using Infrastructure.Exceptions;
using Infrastructure.Message;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserManagement.Contracts.User.Enums;

namespace ProjectManagement.PipelineItems
{
    public class AuthorizationPipelineItem<TCommand> : Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems.AuthorizationPipelineItem<TCommand>
        where TCommand : class, ICommand
    {
        private readonly ProjectManagementContext context;
        private readonly CallContext callContext;

        public AuthorizationPipelineItem(ProjectManagementContext context, CallContext callContext)
        {
            this.context = context;
            this.callContext = callContext;
        }

        public async override Task HandleAsync(TCommand command)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Id == callContext.UserId);
            if (user == null)
                throw new EntityDoesNotExist(callContext.UserId, "User");

            if (user.Role != Role.Admin)
                throw new NotAuthorized(callContext.UserId, typeof(TCommand).Name);

            await base.HandleAsync(command);
        }
    }
}

using ProjectManagement.Infrastructure.CallContexts;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UserManagement.Contracts.User.Enums;
using ProjectManagement.Infrastructure.Primitives.Message;
using ProjectManagementView;
using ProjectManagement.Infrastructure.Message.Pipeline.PipelineItems;

namespace ProjectManagementViews.PipelineItems
{
    public class AuthorizationPipelineItem<TQuery, TResponse> : QueryPipelineItem<TQuery, TResponse>
        where TQuery : class, IQuery<TResponse>
        where TResponse : class
    {
        private readonly ProjectManagementViewContext context;
        private readonly CallContext callContext;

        public AuthorizationPipelineItem(ProjectManagementViewContext context, CallContext callContext)
        {
            this.context = context;
            this.callContext = callContext;
        }

        public async override Task<TResponse> HandleAsync(TQuery command)
        {
            var user = await context.Users.SingleOrDefaultAsync(x => x.Id == callContext.UserId);
            if (user == null)
                throw new EntityDoesNotExist(callContext.UserId, "User");

            if (user.Role != Role.Admin)
                throw new NotAuthorized(callContext.UserId, typeof(TQuery).Name);

            return await base.HandleAsync(command);
        }
    }
}

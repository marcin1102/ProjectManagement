using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infrastructure.CallContexts;
using ProjectManagement.Infrastructure.Message.Pipeline.PipelineItems;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Primitives.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Contracts.User.Enums;

namespace UserManagement.PipelineItems
{
    public class AuthorizationPipelineItem<TQuery, TResponse> : QueryPipelineItem<TQuery, TResponse>
        where TQuery : class, IQuery<TResponse>
        where TResponse : class
    {
        private readonly UserManagementContext context;
        private readonly CallContext callContext;

        public AuthorizationPipelineItem(UserManagementContext context, CallContext callContext)
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

    public class AuthorizationPipelineItem<TCommand> : CommandPipelineItem<TCommand>
        where TCommand : class, ICommand
    {
        private readonly UserManagementContext context;
        private readonly CallContext callContext;

        public AuthorizationPipelineItem(UserManagementContext context, CallContext callContext)
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

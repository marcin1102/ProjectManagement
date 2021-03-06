﻿using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Infrastructure.Primitives.Message;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastructure.Message.Pipeline.PipelineItems
{
    public abstract class CommandPipelineItem<TCommand> : IAsyncCommandHandler<TCommand>
        where TCommand : ICommand
    {
        public IAsyncCommandHandler<TCommand> NextHandler { get; private set; }

        public virtual Task HandleAsync(TCommand command)
        {
            return NextHandler.HandleAsync(command);
        }

        public void SetNextHandler(IAsyncCommandHandler<TCommand> handler)
        {
            NextHandler = handler;
        }

        public CommandPipelineItem<TCommand> SetNextPipelineItem(CommandPipelineItem<TCommand> handler)
        {
            NextHandler = handler;
            return handler;
        }
    }
}

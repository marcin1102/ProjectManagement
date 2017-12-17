using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Infrastructure.Primitives.Message;

namespace ProjectManagement.Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems
{
    public class TransactionalExecutionPipelineItem<TCommand> : CommandPipelineItem<TCommand>
        where TCommand : ICommand
    {
        private readonly DbContext context;

        public TransactionalExecutionPipelineItem(DbContext context)
        {
            this.context = context;
        }

        public override async Task HandleAsync(TCommand command)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    await NextHandler.HandleAsync(command);
                    await context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}

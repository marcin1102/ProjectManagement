using Infrastructure.Message.Handlers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Message.Pipeline.PipelineItems.DefaultCommandPipelineItems
{
    public static class PredefinedCommandPipelines
    {
        public static ICollection<Type> TransactionCommandPipeline => new List<Type>
        {
            typeof(TransactionalExecutionPipelineItem<>)
        };        
    }
}

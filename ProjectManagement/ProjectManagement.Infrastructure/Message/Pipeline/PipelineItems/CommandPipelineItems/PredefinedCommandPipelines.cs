using System;
using System.Collections.Generic;

namespace ProjectManagement.Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems
{
    public static class PredefinedCommandPipelines
    {
        public static IReadOnlyCollection<Type> TransactionalCommandExecutionPipeline() => new List<Type>
        {
            typeof(TransactionalExecutionPipelineItem<>)
        };
    }
}

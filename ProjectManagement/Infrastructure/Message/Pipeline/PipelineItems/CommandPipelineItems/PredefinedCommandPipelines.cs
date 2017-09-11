﻿using System;
using System.Collections.Generic;

namespace Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems
{
    public static class PredefinedCommandPipelines
    {
        public static ICollection<Type> TransactionalCommandExecutionPipeline() => new List<Type>
        {
            typeof(TransactionalExecutionPipelineItem<>)
        };
    }
}

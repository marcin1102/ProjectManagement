using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Infrastructure.Message.Pipeline.PipelineItems.QueryPipelineItems
{
    public static class PredefinedQueryPipelines
    {
        public static IReadOnlyCollection<Type> DefaultQueryPipeline => new List<Type>
        {
            typeof(AuthQueryPipeline<,>)
        };
    }
}

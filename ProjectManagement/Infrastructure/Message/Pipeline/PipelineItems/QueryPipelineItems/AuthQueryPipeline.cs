using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Message.Pipeline.PipelineItems.QueryPipelineItems
{
    public class AuthQueryPipeline<TQuery, TResponse> : QueryPipelineItem<TQuery, TResponse>
        where TQuery : class, IQuery<TResponse>
        where TResponse : class
    {
        public override Task<TResponse> HandleAsync(TQuery query)
        {

            return base.HandleAsync(query);
        }
    }
}

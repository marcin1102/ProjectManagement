using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Message.Handlers;

namespace Infrastructure.Message.Pipeline.PipelineItems
{
    public abstract class QueryPipelineItem<TQuery, TResponse> : IAsyncQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        public IAsyncQueryHandler<TQuery, TResponse> NextHandler { get; private set; }

        public virtual Task<TResponse> HandleAsync(TQuery query)
        {
            return NextHandler.HandleAsync(query);
        }

        public void SetNextHandler(IAsyncQueryHandler<TQuery, TResponse> handler)
        {
            NextHandler = handler;
        }

        public QueryPipelineItem<TQuery, TResponse> SetNextPipelineItem(QueryPipelineItem<TQuery, TResponse> pipelineItem)
        {
            NextHandler = pipelineItem;
            return pipelineItem;
        }
    }
}

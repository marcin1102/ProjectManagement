﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems;
using Infrastructure.Message.Pipeline.PipelineItems.QueryPipelineItems;

namespace Infrastructure.Message.Pipeline.PipelineItems
{
    public class FakePipelineItemsConfiguration : IPipelineItemsConfiguration
    {
        private IDictionary<string, IEnumerable<Type>> commandPipelineItems = new Dictionary<string, IEnumerable<Type>>();
        private IDictionary<string, IEnumerable<Type>> queryPipelineItems = new Dictionary<string, IEnumerable<Type>>();

        public void SetCommandPipeline<TCommand>(IEnumerable<Type> pipelineItems)
            where TCommand : ICommand
        {
            commandPipelineItems[typeof(TCommand).FullName] = pipelineItems.ToList();
        }

        public void SetQueryPipeline<TQuery, TResponse>(IEnumerable<Type> pipelineItems)
            where TQuery : IQuery<TResponse>
        {
            queryPipelineItems[typeof(TQuery).FullName] = pipelineItems.ToList();
        }

        public IEnumerable<Type> GetCommandPipelineItems<TCommand>()
            where TCommand : ICommand
        {
            IEnumerable<Type> pipelineItems;
            return commandPipelineItems.TryGetValue(typeof(TCommand).FullName, out pipelineItems)
                ? pipelineItems
                : new List<Type> { typeof(FakeCommandPIpelineItem<>) };
        }

        public IEnumerable<Type> GetQueryPIpelineITems<TQuery, TResponse>()
            where TQuery : IQuery<TResponse>
        {
            IEnumerable<Type> pipelineItems;
            return commandPipelineItems.TryGetValue(typeof(TQuery).FullName, out pipelineItems)
                ? pipelineItems
                : PredefinedQueryPipelines.DefaultQueryPipeline;
        }
    }
}

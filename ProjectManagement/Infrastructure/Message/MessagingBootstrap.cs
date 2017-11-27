using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.Message.EventDispatcher;
using Infrastructure.Message.Pipeline;
using Infrastructure.Message.Pipeline.PipelineItems;
using Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems;
using Infrastructure.Message.Pipeline.PipelineItems.QueryPipelineItems;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Message
{
    public static class MessagingBootstrap
    {
        public static void RegisterMessagingComponents(this IServiceCollection services)
        {
            services.AddScoped<ICommandQueryBus, CommandQueryBusPipeline>();
            services.AddScoped<PipelineBuilder>();
            services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IEventBroker, EventBroker>();
            services.AddScoped<IEventManager, EventManager>();

            services.RegisterPipelineItems();
        }

        public static void RegisterPipelineItems(this IServiceCollection services)
        {
            services.RegisterPipelineItemsConfiguration();
            services.RegisterPredefinedCommandPipelineItems();
            services.RegisterPredefinedQueryPipelineItems();
        }

        public static void RegisterPipelineItemsConfiguration(this IServiceCollection services)
        {
            services.AddSingleton<IPipelineItemsConfiguration, PipelineItemsConfiguration>();
        }

        public static void RegisterPredefinedCommandPipelineItems(this IServiceCollection services)
        {
            foreach (var item in PredefinedCommandPipelines.TransactionalCommandExecutionPipeline())
            {
                services.AddScoped(item);
            }
        }

        public static void RegisterPredefinedQueryPipelineItems(this IServiceCollection services)
        {
            foreach (var item in PredefinedQueryPipelines.DefaultQueryPipeline)
            {
                services.AddScoped(item);
            }
        }
    }
}

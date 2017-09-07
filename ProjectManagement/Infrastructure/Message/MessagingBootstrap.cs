﻿using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.Message.EventDispatcher;
using Infrastructure.Message.Pipeline;
using Infrastructure.Message.Pipeline.PipelineItems;
using Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems;
using Infrastructure.Message.Pipeline.PipelineItems.QueryPipelineItems;

namespace Infrastructure.Message
{
    public static class MessagingBootstrap
    {
        public static void RegisterMessagingComponents(this ContainerBuilder builder)
        {
            builder
                .RegisterType<CommandQueryBusPipeline>()
                .As<ICommandQueryBus>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<PipelineBuilder>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<DomainEventDispatcher>()
                .As<IDomainEventDispatcher>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<EventBroker>()
                .As<IEventBroker>()
                .InstancePerLifetimeScope();

            builder
                .RegisterType<EventManager>()
                .As<IEventManager>()
                .InstancePerLifetimeScope();


            builder.RegisterPipelineItems();
        }

        public static void RegisterPipelineItems(this ContainerBuilder builder)
        {
            builder.RegisterPipelineItemsConfiguration();
            builder.RegisterPredefinedCommandPipelineItems();
            builder.RegisterPredefinedQueryPipelineItems();
        }

        public static void RegisterPipelineItemsConfiguration(this ContainerBuilder builder)
        {
            builder
                .RegisterType<PipelineItemsConfiguration>()
                .SingleInstance();
        }

        public static void RegisterPredefinedCommandPipelineItems(this ContainerBuilder builder)
        {
            foreach (var item in PredefinedCommandPipelines.TransactionalCommandExecutionPipeline)
            {
                builder
                    .RegisterGeneric(item)
                    .InstancePerLifetimeScope();
            }
        }

        public static void RegisterPredefinedQueryPipelineItems(this ContainerBuilder builder)
        {
            foreach (var item in PredefinedQueryPipelines.DefaultQueryPipeline)
            {
                builder
                    .RegisterGeneric(item)
                    .InstancePerLifetimeScope();
            }
        }
    }
}

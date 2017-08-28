using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.Message.Pipeline;
using Infrastructure.Message.Pipeline.PipelineItems.DefaultCommandPipelineItems;
using Infrastructure.Message;

namespace Infrastructure.Bootstrap
{
    public static class InfrastructureBootstrap
    {
        public static void RegisterInfrastructureComponents(this ContainerBuilder builder)
        {
            builder
                .RegisterType<CommandQueryBusPipeline>()
                .As<ICommandQueryBus>()
                .InstancePerDependency();

            builder
                .RegisterType<PipelineBuilder>()
                .AsSelf()
                .InstancePerDependency();

            builder.RegisterPredefinedPipelineItems();
        }

        public static void RegisterPredefinedPipelineItems(this ContainerBuilder builder)
        {
            foreach (var item in PredefinedCommandPipelines.TransactionCommandPipeline)
            {
                builder
                    .RegisterGeneric(item)
                    .InstancePerDependency();
            }
        }
    }
}

using Autofac;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.Message.EventDispatcher;
using Infrastructure.Message.Pipeline;
using Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems;
using Infrastructure.Message.Pipeline.PipelineItems.QueryPipelineItems;
using Infrastructure.Settings;
using Infrastructure.Storage.EF;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Bootstrap
{
    public static class InfrastructureBootstrap
    {
        public static void RegisterInfrastructureComponents(this ContainerBuilder builder, IConfigurationRoot configuration)
        {
            builder.RegisterSettings(configuration);
            builder.RegisterEfComponents(configuration);

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

            builder.RegisterPredefinedPipelineItems();
        }

        public static void RegisterPredefinedPipelineItems(this ContainerBuilder builder)
        {
            builder.RegisterPredefinedCommandPipelineItems();
            builder.RegisterPredefinedQueryPipelineItems();
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

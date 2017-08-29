using Autofac;
using Infrastructure.Message.CommandQueryBus;
using Infrastructure.Message.Pipeline;
using Infrastructure.Message.Pipeline.PipelineItems.CommandPipelineItems;
using Infrastructure.Message.Pipeline.PipelineItems.QueryPipelineItems;

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
            builder.RegisterPredefinedCommandPipelineItems();
            builder.RegisterPredefinedQueryPipelineItems();
        }

        public static void RegisterPredefinedCommandPipelineItems(this ContainerBuilder builder)
        {
            foreach (var item in PredefinedCommandPipelines.TransactionalCommandExecutionPipeline)
            {
                builder
                    .RegisterGeneric(item)
                    .InstancePerDependency();
            }
        }

        public static void RegisterPredefinedQueryPipelineItems(this ContainerBuilder builder)
        {
            foreach (var item in PredefinedQueryPipelines.DefaultQueryPipeline)
            {
                builder
                    .RegisterGeneric(item)
                    .InstancePerDependency();
            }
        }
    }
}

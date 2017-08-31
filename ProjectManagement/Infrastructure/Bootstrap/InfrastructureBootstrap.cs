using Autofac;
using Infrastructure.Message;
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
            builder.RegisterMessagingComponents();
        }
    }
}

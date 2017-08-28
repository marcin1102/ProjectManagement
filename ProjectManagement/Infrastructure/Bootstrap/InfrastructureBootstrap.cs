using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Infrastructure.Message.CommandQueryBus;

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
        }
    }
}

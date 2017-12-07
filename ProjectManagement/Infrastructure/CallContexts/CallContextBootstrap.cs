using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.CallContexts
{
    public static class CallContextBootstrap
    {
        public static void RegisterCallContext(this ContainerBuilder builder)
        {
            builder
                .RegisterType<CallContext>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}

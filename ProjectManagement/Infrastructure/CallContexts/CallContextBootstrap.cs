using Autofac;

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

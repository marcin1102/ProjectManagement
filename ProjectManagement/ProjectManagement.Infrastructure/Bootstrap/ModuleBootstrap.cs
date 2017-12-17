using System.Reflection;
using Autofac;
using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Infrastructure.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ProjectManagement.Infrastructure.Primitives.Message;

namespace ProjectManagement.Infrastructure.Bootstrap
{
    public abstract class ModuleBootstrap
    {
        protected ContainerBuilder builder { get; private set; }
        protected IConfigurationRoot configuration;
        protected ILoggerFactory logger;
        protected IComponentContext context;

        protected ModuleBootstrap(ContainerBuilder builder, IConfigurationRoot configuration, ILoggerFactory logger)
        {
            this.builder = builder;
            this.configuration = configuration;
            this.logger = logger;

            RegisterCommandHandlers();
            RegisterEventHandlers();
            RegisterQueryHandlers();
            RegisterPipelineItems();

            AddAssemblyToProvider();
        }

        public virtual void Run(IComponentContext context)
        {
            this.context = context;
            RegisterCommandPipelines();
            RegisterQueryPipelines();
        }

        public abstract void RegisterCommandHandlers();
        public abstract void RegisterQueryHandlers();
        public abstract void RegisterEventHandlers();

        public virtual void RegisterPipelineItems() { }
        public virtual void RegisterCommandPipelines() { }
        public virtual void RegisterQueryPipelines() { }

        public virtual void AddAssemblyToProvider()
        {
            AssembliesProvider.assemblies.Add(this.GetType().GetTypeInfo().Assembly);
        }

        protected void RegisterAsyncCommandHandler<TCommand, THandler>()
            where TCommand : class, ICommand
            where THandler : class, IAsyncCommandHandler<TCommand>
        {
            builder
                .RegisterType<THandler>()
                .As<IAsyncCommandHandler<TCommand>>()
                .InstancePerLifetimeScope();
        }

        protected void RegisterAsyncQueryHandler<TQuery, TResponse , THandler>()
            where TQuery : class, IQuery<TResponse>
            where THandler : class, IAsyncQueryHandler<TQuery, TResponse>
        {
            builder
                .RegisterType<THandler>()
                .As<IAsyncQueryHandler<TQuery, TResponse>>()
                .InstancePerLifetimeScope();
        }

        protected void RegisterAsyncEventHandler<TEvent, THandler>()
            where TEvent : class, IDomainEvent
            where THandler : class, IAsyncEventHandler<TEvent>
        {
            builder
                .RegisterType<THandler>()
                .As<IAsyncEventHandler<TEvent>>()
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}

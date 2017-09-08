using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Autofac;
using Infrastructure.Message;
using Infrastructure.Message.Handlers;
using Infrastructure.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Bootstrap
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

        public void Run(IComponentContext context)
        {
            this.context = context;
            RegisterCommandPipelines();
        }

        public abstract void RegisterCommandHandlers();
        public abstract void RegisterQueryHandlers();
        public abstract void RegisterEventHandlers();

        public virtual void RegisterPipelineItems() { }
        public virtual void RegisterCommandPipelines() { }
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

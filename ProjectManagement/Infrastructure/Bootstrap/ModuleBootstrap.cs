using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Infrastructure.Message;
using Infrastructure.Message.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Bootstrap
{
    public abstract class ModuleBootstrap
    {
        public ContainerBuilder builder { get; private set; }
        protected ModuleBootstrap(ContainerBuilder builder)
        {
            this.builder = builder;

            RegisterCommandHandlers();
            RegisterEventHandlers();
            RegisterQueryHandlers();
        }

        public abstract void RegisterCommandHandlers();
        public abstract void RegisterQueryHandlers();
        public abstract void RegisterEventHandlers();

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

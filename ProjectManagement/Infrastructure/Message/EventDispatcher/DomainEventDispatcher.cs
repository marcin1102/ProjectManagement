using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Infrastructure.Message.Handlers;
using Infrastructure.Providers;
using Microsoft.Extensions.PlatformAbstractions;

namespace Infrastructure.Message.EventDispatcher
{
    public class DomainEventDispatcher : IDomainEventDispatcher
    {
        private readonly IComponentContext container;

        public DomainEventDispatcher(IComponentContext container)
        {
            this.container = container;
        }

        public async Task Dispatch<TEvent>(TEvent @event)
            where TEvent : IDomainEvent
        {
            var assembliesNames = Assembly.GetEntryAssembly().GetReferencedAssemblies();
            var projectsNames = ProjectsNamesProvider.GetDomainProjectsNames();
            var assemblies = assembliesNames.Where(assemblyName => projectsNames.Any(projectName => assemblyName.Name == projectName))
                .Select(x => Assembly.Load(x));

            foreach (var assembly in assemblies)
            {
                var broker = new EventBroker(container);
                await broker.DeliverEventTo(@event, assembly);
            }
        }
    }
}

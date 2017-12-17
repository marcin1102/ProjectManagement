using ProjectManagement.Infrastructure.CallContexts;
using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagementView.Contracts.Projects;
using ProjectManagementView.Searchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Handlers.Projects
{
    public class ProjectQueryHandler :
        IAsyncQueryHandler<GetProjects, IReadOnlyCollection<ProjectListItem>>,
        IAsyncQueryHandler<GetProjectsAsAdmin, IReadOnlyCollection<ProjectListItem>>

    {
        private readonly IProjectSearcher projectSearcher;
        private readonly CallContext callContext;

        public ProjectQueryHandler(IProjectSearcher projectSearcher, CallContext callContext)
        {
            this.projectSearcher = projectSearcher;
            this.callContext = callContext;
        }

        public async Task<IReadOnlyCollection<ProjectListItem>> HandleAsync(GetProjects query)
        {
            var projects = await projectSearcher.GetProjects(callContext.UserId);
            return projects.Select(x => new ProjectListItem(x.Id, x.Name)).ToList();
        }

        public async Task<IReadOnlyCollection<ProjectListItem>> HandleAsync(GetProjectsAsAdmin query)
        {
            var projects = await projectSearcher.GetProjects();
            return projects.Select(x => new ProjectListItem(x.Id, x.Name)).ToList();
        }
    }
}

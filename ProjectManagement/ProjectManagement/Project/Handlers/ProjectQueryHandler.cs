using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Project.Queries;
using ProjectManagement.Project.Repository;
using ProjectManagement.Project.Searchers;

namespace ProjectManagement.Project.Handlers
{
    public class ProjectQueryHandler :
        IAsyncQueryHandler<GetProject, ProjectResponse>,
        IAsyncQueryHandler<GetProjects, ICollection<ProjectResponse>>
    {
        private readonly ProjectRepository repository;
        private readonly IProjectSearcher searcher;

        public ProjectQueryHandler(ProjectRepository repository, IProjectSearcher searcher)
        {
            this.repository = repository;
            this.searcher = searcher;
        }

        public async Task<ProjectResponse> HandleAsync(GetProject query)
        {
            var project = await repository.FindAsync(query.Id);

            if (project == null)
                throw new EntityDoesNotExist(query.Id, nameof(Model.Project));

            return new ProjectResponse(project.Id, project.Name, project.Version);
        }

        public async Task<ICollection<ProjectResponse>> HandleAsync(GetProjects query)
        {
            var projects = await searcher.GetProjects();
            return projects.Select(x => new ProjectResponse(x.Id, x.Name, x.Version)).ToList();
        }
    }
}

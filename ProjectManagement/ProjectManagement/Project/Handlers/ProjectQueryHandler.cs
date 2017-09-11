using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Project.Queries;
using ProjectManagement.Project.Repository;

namespace ProjectManagement.Project.Handlers
{
    public class ProjectQueryHandler :
        IAsyncQueryHandler<GetProject, ProjectResponse>
    {
        private readonly ProjectRepository repository;

        public ProjectQueryHandler(ProjectRepository repository)
        {
            this.repository = repository;
        }

        public async Task<ProjectResponse> HandleAsync(GetProject query)
        {
            var project = await repository.FindAsync(query.Id);

            if (project == null)
                throw new EntityDoesNotExist(query.Id, nameof(Model.Project));

            return new ProjectResponse(project.Id, project.Name, project.Version);
        }
    }
}

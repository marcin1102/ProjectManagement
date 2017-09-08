using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

            return new ProjectResponse(project.Id, project.Name, project.Version);
        }
    }
}

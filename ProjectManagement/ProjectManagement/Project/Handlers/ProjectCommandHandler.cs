using System;
using System.Threading.Tasks;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Project.Repository;

namespace ProjectManagement.Project.Handlers
{
    public class ProjectCommandHandler : IAsyncCommandHandler<CreateProject>
    {
        private readonly ProjectRepository repository;

        public ProjectCommandHandler(ProjectRepository repository)
        {
            this.repository = repository;
        }

        public Task HandleAsync(CreateProject command)
        {
            command.CreatedId = Guid.NewGuid();
            var project = new Model.Project(command.CreatedId, command.Name);
            return repository.AddAsync(project);
        }
    }
}

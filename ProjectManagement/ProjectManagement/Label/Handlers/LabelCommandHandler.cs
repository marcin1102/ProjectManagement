using System;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Label.Commands;
using ProjectManagement.Label.Repository;
using ProjectManagement.Project.Repository;

namespace ProjectManagement.Label.Handlers
{
    public class LabelCommandHandler : IAsyncCommandHandler<CreateLabel>
    {
        private readonly LabelRepository repository;
        private readonly ProjectRepository projectRepository;

        public LabelCommandHandler(LabelRepository repository, ProjectRepository projectRepository)
        {
            this.repository = repository;
            this.projectRepository = projectRepository;
        }

        public async Task HandleAsync(CreateLabel command)
        {
            var project = await projectRepository.FindAsync(command.ProjectId);

            if (project == null)
                throw new EntityDoesNotExist(command.ProjectId, nameof(Project.Model.Project));

            command.CreatedId = Guid.NewGuid();
            await repository.AddAsync(new Label(command.CreatedId, command.ProjectId, command.Name, command.Description));
        }
    }
}

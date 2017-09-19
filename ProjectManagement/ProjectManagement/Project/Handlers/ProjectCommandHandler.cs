using System;
using System.Threading.Tasks;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Project.Repository;
using ProjectManagement.User.Repository;

namespace ProjectManagement.Project.Handlers
{
    public class ProjectCommandHandler :
        IAsyncCommandHandler<CreateProject>,
        IAsyncCommandHandler<AssignUserToProject>
    {
        private readonly ProjectRepository projectRepository;
        private readonly UserRepository userRepository;

        public ProjectCommandHandler(ProjectRepository projectRepository, UserRepository userRepository)
        {
            this.projectRepository = projectRepository;
            this.userRepository = userRepository;
        }

        public Task HandleAsync(CreateProject command)
        {
            command.CreatedId = Guid.NewGuid();
            var project = new Model.Project(command.CreatedId, command.Name);
            project.Created();
            return projectRepository.AddAsync(project, project.Version);
        }

        public async Task HandleAsync(AssignUserToProject command)
        {
            var project = await projectRepository.GetAsync(command.ProjectId);

            project.AssignUser(command.UserToAssignId);
            await projectRepository.Update(project, command.ProjectVersion);
        }
    }
}

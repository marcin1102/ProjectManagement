using System;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Project.Factory;
using ProjectManagement.Project.Repository;
using ProjectManagement.Services;
using ProjectManagement.User.Repository;

namespace ProjectManagement.Project.Handlers
{
    public class ProjectCommandHandler :
        IAsyncCommandHandler<CreateProject>,
        IAsyncCommandHandler<AssignUserToProject>,
        IAsyncCommandHandler<AddLabel>
    {
        private readonly ProjectRepository projectRepository;
        private readonly UserRepository userRepository;
        private readonly IAuthorizationService authorizationService;
        private readonly IProjectFactory projectFactory;

        public ProjectCommandHandler(ProjectRepository projectRepository, UserRepository userRepository, IAuthorizationService authorizationService, IProjectFactory projectFactory)
        {
            this.projectRepository = projectRepository;
            this.userRepository = userRepository;
            this.authorizationService = authorizationService;
            this.projectFactory = projectFactory;
        }

        public async Task HandleAsync(CreateProject command)
        {
            var project = await projectFactory.GenerateProject(command);
            await projectRepository.AddAsync(project);
        }

        public async Task HandleAsync(AssignUserToProject command)
        {
            var project = await projectRepository.GetAsync(command.ProjectId);
            project.AssignUser(userRepository, command);
            await projectRepository.Update(project, command.ProjectVersion);
        }

        public async Task HandleAsync(AddLabel command)
        {
            var project = await projectRepository.GetAsync(command.ProjectId);
            var projectVersion = project.Version;
            project.AddLabel(command);
            await projectRepository.Update(project, projectVersion);
        }
    }
}

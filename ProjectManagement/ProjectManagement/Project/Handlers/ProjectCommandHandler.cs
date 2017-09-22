using System;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Project.Repository;
using ProjectManagement.Services;
using ProjectManagement.User.Repository;

namespace ProjectManagement.Project.Handlers
{
    public class ProjectCommandHandler :
        IAsyncCommandHandler<CreateProject>,
        IAsyncCommandHandler<AssignUserToProject>
    {
        private readonly ProjectRepository projectRepository;
        private readonly UserRepository userRepository;
        private readonly IAuthorizationService authorizationService;

        public ProjectCommandHandler(ProjectRepository projectRepository, UserRepository userRepository, IAuthorizationService authorizationService)
        {
            this.projectRepository = projectRepository;
            this.userRepository = userRepository;
            this.authorizationService = authorizationService;
        }

        public async Task HandleAsync(CreateProject command)
        {
            await authorizationService.CheckUserRole(command.AdminId, nameof(CreateProject));

            command.CreatedId = Guid.NewGuid();
            var project = new Model.Project(command.CreatedId, command.Name);
            project.Created();
            await projectRepository.AddAsync(project);
        }

        public async Task HandleAsync(AssignUserToProject command)
        {
            await authorizationService.CheckUserRole(command.AdminId, nameof(CreateProject));
            await CheckIfUserExistsInSystem(command.UserToAssignId);

            var project = await projectRepository.GetAsync(command.ProjectId);
            project.AssignUser(command.UserToAssignId);
            await projectRepository.Update(project, command.ProjectVersion);
        }

        private async Task CheckIfUserExistsInSystem(Guid userId)
        {
            var user = await userRepository.FindAsync(userId);
            if (user == null)
                throw new EntityDoesNotExist(userId, nameof(User.Model.User));
        }
    }
}

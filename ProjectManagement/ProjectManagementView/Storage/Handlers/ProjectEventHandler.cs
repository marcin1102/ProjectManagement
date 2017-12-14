using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using Infrastructure.Storage.EF.Repository;
using ProjectManagement.Contracts.Project.Events;

namespace ProjectManagementView.Storage.Handlers
{
    public class ProjectEventHandler :
        IAsyncEventHandler<ProjectCreated>,
        IAsyncEventHandler<UserAssignedToProject>,
        IAsyncEventHandler<LabelAdded>
    {
        private readonly IRepository<Models.Project> projectRepository;
        private readonly IRepository<Models.User> userRepository;
        private readonly IRepository<Models.Label> labelRepository;

        public ProjectEventHandler(IRepository<Models.Project> projectRepository, IRepository<Models.User> userRepository, IRepository<Models.Label> labelRepository)
        {
            this.projectRepository = projectRepository;
            this.userRepository = userRepository;
            this.labelRepository = labelRepository;
        }

        public async Task HandleAsync(ProjectCreated @event)
        {
            var project = await projectRepository.FindAsync(@event.Id);
            if (project != null)
                throw new EntityAlreadyExist(@event.Id, nameof(Models.Project));

            project = new Models.Project(@event.Id)
            {
                Name = @event.Name,
                Version = @event.AggregateVersion
            };

            await projectRepository.AddAsync(project);
        }

        public async Task HandleAsync(UserAssignedToProject @event)
        {
            var project = await projectRepository.GetAsync(@event.ProjectId);
            var user = await userRepository.GetAsync(@event.UserId);

            project.Users.Add(user);
            project.Version = @event.AggregateVersion;
            await projectRepository.Update(project);
        }

        public async Task HandleAsync(LabelAdded @event)
        {
            var project = await projectRepository.GetAsync(@event.ProjectId);
            project.Labels.Add(new Models.Label(@event.Id, @event.Name, @event.Description));
            await projectRepository.Update(project);
        }
    }
}

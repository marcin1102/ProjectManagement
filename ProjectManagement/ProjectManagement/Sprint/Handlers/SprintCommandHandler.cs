using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Sprint.Commands;
using ProjectManagement.Project.Searchers;
using ProjectManagement.Sprint.Factory;
using ProjectManagement.Sprint.Repository;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Sprint.Handlers
{
    public class SprintCommandHandler :
        IAsyncCommandHandler<CreateSprint>,
        IAsyncCommandHandler<StartSprint>,
        IAsyncCommandHandler<FinishSprint>
    {
        private readonly SprintRepository sprintRepository;
        private readonly ISprintFactory sprintFactory;

        public SprintCommandHandler(SprintRepository sprintRepository, ISprintFactory sprintFactory)
        {
            this.sprintRepository = sprintRepository;
            this.sprintFactory = sprintFactory;
        }

        public async Task HandleAsync(CreateSprint command)
        {
            var sprint = await sprintFactory.GenerateSprint(command);
            await sprintRepository.AddAsync(sprint);
        }

        public async Task HandleAsync(StartSprint command)
        {
            var sprint = await sprintRepository.GetAsync(command.Id);
            var originalVersion = sprint.Version;

            sprint.StartSprint();
            await sprintRepository.Update(sprint, originalVersion);
        }

        public async Task HandleAsync(FinishSprint command)
        {
            var sprint = await sprintRepository.GetAsync(command.Id);
            var originalVersion = sprint.Version;

            sprint.FinishSprint();
            await sprintRepository.Update(sprint, originalVersion);
        }
    }
}

﻿using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Sprint.Commands;
using ProjectManagement.Project.Searchers;
using ProjectManagement.Sprint.Factory;
using ProjectManagement.Sprint.Repository;
using System;
using System.Threading.Tasks;
using ProjectManagement.Sprint.Searchers;

namespace ProjectManagement.Sprint.Handlers
{
    public class SprintCommandHandler :
        IAsyncCommandHandler<CreateSprint>,
        IAsyncCommandHandler<StartSprint>,
        IAsyncCommandHandler<FinishSprint>
    {
        private readonly SprintRepository sprintRepository;
        private readonly ISprintFactory sprintFactory;
        private readonly ISprintSearcher sprintSearcher;

        public SprintCommandHandler(SprintRepository sprintRepository, ISprintFactory sprintFactory, ISprintSearcher sprintSearcher)
        {
            this.sprintRepository = sprintRepository;
            this.sprintFactory = sprintFactory;
            this.sprintSearcher = sprintSearcher;
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

            await sprint.StartSprint(sprintSearcher);
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

﻿using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Sprint.Commands;
using ProjectManagement.Project.Searchers;
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
        private readonly IProjectSearcher projectSearcher;

        public SprintCommandHandler(SprintRepository sprintRepository)
        {
            this.sprintRepository = sprintRepository;
        }

        public SprintCommandHandler(SprintRepository sprintRepository, IProjectSearcher projectSearcher)
        {
            this.sprintRepository = sprintRepository;
            this.projectSearcher = projectSearcher;
        }

        public async Task HandleAsync(CreateSprint command)
        {
            var doesProjectExist = await projectSearcher.DoesProjectExist(command.ProjectId);
            if (!doesProjectExist)
                throw new EntityDoesNotExist(command.ProjectId, nameof(Project.Model.Project));

            command.CreatedId = Guid.NewGuid();
            var sprint = new Model.Sprint(command.CreatedId, command.ProjectId, command.Name, command.Start.Date, command.End.Date);

            sprint.Created();
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

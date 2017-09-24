using Infrastructure.Exceptions;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Sprint.Queries;
using ProjectManagement.Sprint.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagement.Sprint.Handlers
{
    public class SprintQueryHandler :
        IAsyncQueryHandler<GetSprint, SprintResponse>
    {
        private readonly SprintRepository sprintRepository;

        public SprintQueryHandler(SprintRepository sprintRepository)
        {
            this.sprintRepository = sprintRepository;
        }

        public async Task<SprintResponse> HandleAsync(GetSprint query)
        {
            var sprint = await sprintRepository.FindAsync(query.Id);
            if (sprint == null)
                throw new EntityDoesNotExist(query.Id, nameof(Model.Sprint));

            return new SprintResponse(sprint.Id, sprint.ProjectId, sprint.Name, sprint.StartDate.Date, sprint.EndDate.Date, sprint.Status, sprint.UnfinishedIssues);
        }
    }
}

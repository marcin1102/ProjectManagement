using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagement.Infrastructure.Storage.EF.Repository;
using ProjectManagementView.Contracts.Projects.Sprints;
using ProjectManagementView.Searchers;
using ProjectManagementView.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Handlers.Sprints
{
    public class SprintQueryHandler :
        IAsyncQueryHandler<GetSprints, IReadOnlyCollection<SprintListItem>>,
        IAsyncQueryHandler<GetSprint, SprintResponse>
    {
        private readonly ISprintSearcher sprintSearcher;
        private readonly IRepository<Sprint> repository;

        public SprintQueryHandler(ISprintSearcher sprintSearcher, IRepository<Sprint> repository)
        {
            this.sprintSearcher = sprintSearcher;
            this.repository = repository;
        }

        public async Task<IReadOnlyCollection<SprintListItem>> HandleAsync(GetSprints query)
        {
            var sprints = await sprintSearcher.GetSprints(query.ProjectId, query.NotFinishedOnly);
            return sprints.Select(x => new SprintListItem(x.Id, x.Name, x.Start, x.End, x.Status)).ToList();
        }

        public async Task<SprintResponse> HandleAsync(GetSprint query)
        {
            var sprint = await repository.GetAsync(query.SprintId);
            return new SprintResponse(sprint.Id, sprint.Name, sprint.Start, sprint.End, sprint.Status, sprint.UnfinishedIssues, sprint.Version);
        }
    }
}

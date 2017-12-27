using ProjectManagement.Infrastructure.Message.Handlers;
using ProjectManagementView.Contracts.Projects.Sprints;
using ProjectManagementView.Searchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Handlers.Sprints
{
    public class SprintQueryHandler :
        IAsyncQueryHandler<GetSprints, IReadOnlyCollection<SprintListItem>>
    {
        private readonly ISprintSearcher sprintSearcher;

        public SprintQueryHandler(ISprintSearcher sprintSearcher)
        {
            this.sprintSearcher = sprintSearcher;
        }

        public async Task<IReadOnlyCollection<SprintListItem>> HandleAsync(GetSprints query)
        {
            var sprints = await sprintSearcher.GetSprints(query.ProjectId, query.NotFinishedOnly);
            return sprints.Select(x => new SprintListItem(x.Id, x.Name, x.Start, x.End, x.Status)).ToList();
        }
    }
}

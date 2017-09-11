using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Label.Queries;
using ProjectManagement.Label.Repository;

namespace ProjectManagement.Label.Handlers
{
    public class LabelQueryHandler : IAsyncQueryHandler<GetLabel, LabelResponse>
    {
        private readonly LabelRepository repository;

        public LabelQueryHandler(LabelRepository repository)
        {
            this.repository = repository;
        }

        public async Task<LabelResponse> HandleAsync(GetLabel query)
        {
            var label = await repository.GetAsync(query.Id);
            return new LabelResponse(label.Id, label.ProjectId, label.Name, label.Description);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Label.Queries;
using ProjectManagement.Label.Repository;
using ProjectManagement.Label.Searcher;

namespace ProjectManagement.Label.Handlers
{
    public class LabelQueryHandler :
        IAsyncQueryHandler<GetLabel, LabelResponse>,
        IAsyncQueryHandler<GetLabels, ICollection<LabelResponse>>
    {
        private readonly LabelRepository repository;
        private readonly ILabelsSearcher searcher;

        public LabelQueryHandler(LabelRepository repository, ILabelsSearcher searcher)
        {
            this.repository = repository;
            this.searcher = searcher;
        }

        public async Task<LabelResponse> HandleAsync(GetLabel query)
        {
            var label = await repository.GetAsync(query.Id);
            return new LabelResponse(label.Id, label.ProjectId, label.Name, label.Description);
        }

        public async Task<ICollection<LabelResponse>> HandleAsync(GetLabels query)
        {
            var labels = await searcher.GetLabels(query.ProjectId);
            return labels.Select(x => new LabelResponse(x.Id, x.ProjectId, x.Name, x.Description)).ToList();
        }
    }
}

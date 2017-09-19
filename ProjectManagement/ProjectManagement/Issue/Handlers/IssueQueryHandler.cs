using System;
using System.Threading.Tasks;
using Infrastructure.Message.Handlers;
using ProjectManagement.Contracts.Issue.Queries;
using ProjectManagement.Issue.Repository;

namespace ProjectManagement.Issue.Handlers
{
    public class IssueQueryHandler :
        IAsyncQueryHandler<GetIssue, IssueResponse>
    {
        private readonly IssueRepository repository;

        public IssueQueryHandler(IssueRepository repository)
        {
            this.repository = repository;
        }

        public Task<IssueResponse> HandleAsync(GetIssue query)
        {
            throw new NotImplementedException();
        }
    }
}

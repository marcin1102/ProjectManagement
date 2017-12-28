using Newtonsoft.Json;
using ProjectManagement.Infrastructure.Primitives.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Contracts.Issues.Abstract
{
    public abstract class GetIssuesRelatedTo : IQuery<IReadOnlyCollection<IssueListItem>>
    {
        protected GetIssuesRelatedTo(Guid projectId, Guid parentIssueId)
        {
            ProjectId = projectId;
            ParentIssueId = parentIssueId;
        }
        [JsonIgnore]
        public Guid ProjectId { get; private set; }
        [JsonIgnore]
        public Guid ParentIssueId { get; private set; }
    }
}

using Infrastructure.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Contracts.Issue.Queries
{
    public class GetUnfinishedIssues : IQuery<ICollection<IssueListItem>>
    {
        public Guid SprintId { get; set; }
    }
}

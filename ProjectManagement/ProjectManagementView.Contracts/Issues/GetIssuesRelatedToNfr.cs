using ProjectManagementView.Contracts.Issues.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Contracts.Issues
{
    public class GetIssuesRelatedToNfr : GetIssuesRelatedTo
    {
        public GetIssuesRelatedToNfr(Guid projectId, Guid nfrId) : base(projectId, nfrId)
        {
        }
    }
}

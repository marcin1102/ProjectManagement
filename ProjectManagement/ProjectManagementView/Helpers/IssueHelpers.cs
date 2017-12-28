using ProjectManagementView.Contracts.Issues.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Helpers
{
    public static class IssueHelpers
    {
        public static IssueType GetIssueType(Storage.Models.Abstract.Issue issue)
        {
            if (typeof(Storage.Models.Task) == issue.GetType())
                return IssueType.Task;

            if (typeof(Storage.Models.Nfr) == issue.GetType())
                return IssueType.Nfr;

            if (typeof(Storage.Models.Bug) == issue.GetType())
                return IssueType.Bug;

            return IssueType.Subtask;
        }
    }
}

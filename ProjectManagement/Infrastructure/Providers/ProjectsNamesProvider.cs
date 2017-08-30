using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Providers
{
    public static class ProjectsNamesProvider
    {
        public static IReadOnlyCollection<string> GetDomainProjectsNames() => new List<string>
        {
            "ProjectManagement",
            "UserManagement"
        };
    }
}

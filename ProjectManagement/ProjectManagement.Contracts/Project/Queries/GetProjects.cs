using System.Collections.Generic;
using ProjectManagement.Infrastructure.Primitives.Message;

namespace ProjectManagement.Contracts.Project.Queries
{
    public class GetProjects : IQuery<ICollection<ProjectResponse>>
    {
    }
}

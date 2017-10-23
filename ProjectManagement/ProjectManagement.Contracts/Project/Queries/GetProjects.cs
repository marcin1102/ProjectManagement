using System.Collections.Generic;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Project.Queries
{
    public class GetProjects : IQuery<ICollection<ProjectResponse>>
    {
    }
}

using ProjectManagement.Infrastructure.Primitives.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Contracts.Projects
{
    public class GetProjects : IQuery<IReadOnlyCollection<ProjectListItem>>
    {
        
    }

    public class GetProjectsAsAdmin : GetProjects
    {

    }

    public class ProjectListItem
    {
        public ProjectListItem(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}

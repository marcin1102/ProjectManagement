using System;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Message.EventDispatcher;
using ProjectManagement.Infrastructure.Storage.EF.Repository;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.Project.Model;

namespace ProjectManagement.Project.Repository
{
    public class ProjectRepository : AggregateRepository<Model.Project>
    {
        public ProjectRepository(ProjectManagementContext context, IEventManager eventManager) : base(context, eventManager)
        {
        }

        public override Task<Model.Project> GetAsync(Guid id)
        {
            return Query.SingleOrDefaultAsync(x => x.Id == id) ?? throw new EntityDoesNotExist(id, nameof(Model.Project));
        }
    }
}

using System;
using System.Threading.Tasks;
using Infrastructure.Exceptions;
using Infrastructure.Message.EventDispatcher;
using Infrastructure.Storage.EF.Repository;
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
            return Query.Include(x => x.Members).SingleOrDefaultAsync(x => x.Id == id) ?? throw new EntityDoesNotExist(id, nameof(Model.Project));
        }
    }
}

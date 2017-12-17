using ProjectManagement.Infrastructure.Storage.EF.Repository;

namespace ProjectManagement.Label.Repository
{
    public class LabelRepository : Repository<Label>
    {
        public LabelRepository(ProjectManagementContext db) : base(db)
        {
        }
    }
}

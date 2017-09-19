using Infrastructure.Storage.EF.Repository;

namespace ProjectManagement.User.Repository
{
    public class UserRepository : Repository<Model.User>
    {
        public UserRepository(ProjectManagementContext dbContext) : base(dbContext)
        {
        }
    }
}

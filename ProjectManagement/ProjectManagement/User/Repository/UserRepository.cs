using ProjectManagement.Infrastructure.Storage.EF.Repository;

namespace ProjectManagement.User.Repository
{
    public class UserRepository : Repository<Model.Member>
    {
        public UserRepository(ProjectManagementContext dbContext) : base(dbContext)
        {
        }
    }
}

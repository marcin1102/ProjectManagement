using ProjectManagement.Infrastructure.Message.EventDispatcher;
using ProjectManagement.Issue.Model;

namespace ProjectManagement.Issue.Repository
{
    public class BugRepository : IssueRepository<Bug>
    {
        public BugRepository(ProjectManagementContext dbContext, IEventManager eventManager) : base(dbContext, eventManager)
        {
        }
    }
}
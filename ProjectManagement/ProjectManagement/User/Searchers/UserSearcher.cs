using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.User.Model;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProjectManagement.User.Searchers
{
    public interface IUserSearcher
    {
        Task<List<Model.User>> GetUnfinishedIssuesAssignees(Guid sprintId);
    }

    public class UserSearcher : IUserSearcher
    {
        private readonly ProjectManagementContext db;

        public UserSearcher(ProjectManagementContext db)
        {
            this.db = db;
        }

        public async Task<List<Model.User>> GetUnfinishedIssuesAssignees(Guid sprintId)
        {
            var sprint = await db.Sprints.SingleAsync(x => x.Id == sprintId);
            var assigneesIds = sprint.UnfinishedIssues.Select(x => x.UserId);
            return await db.Users.Where(x => assigneesIds.Contains(x.Id)).ToListAsync();
        }
    }
}

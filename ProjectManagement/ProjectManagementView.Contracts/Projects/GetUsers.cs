using ProjectManagement.Infrastructure.Primitives.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Contracts.Projects
{
    public class GetUsers : IQuery<IReadOnlyCollection<UserData>>
    {
        public GetUsers(Guid projectId)
        {
            ProjectId = projectId;
        }

        public Guid ProjectId { get; private set; }
    }

    public class UserData
    {
        public UserData(Guid id, string firstName, string lastName, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public Guid Id { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
    }
}

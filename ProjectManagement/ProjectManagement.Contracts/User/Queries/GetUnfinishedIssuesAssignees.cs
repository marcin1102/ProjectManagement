using Infrastructure.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Contracts.User.Queries
{
    public class GetUnfinishedIssuesAssignees : IQuery<ICollection<UserListItem>>
    {
        public Guid SprintId { get; set; }
    }

    public class UserListItem
    {
        public UserListItem(Guid id, string firstName, string lastName, string email)
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

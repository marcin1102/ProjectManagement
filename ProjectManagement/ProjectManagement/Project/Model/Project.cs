using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Storage;
using ProjectManagement.Contracts.Project.Events;
using ProjectManagement.Contracts.Project.Exceptions;
using ProjectManagement.User.Model;

namespace ProjectManagement.Project.Model
{
    public class Project : AggregateRoot
    {
        private Project() { }

        public Project(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; private set; }

        private List<ProjectUser.ProjectUser> members = new List<ProjectUser.ProjectUser>();
        public IEnumerable<ProjectUser.ProjectUser> Members => members;

        public void AssignUser(Guid userId)
        {
            CheckIfUserIsAlreadyAssigned(userId);

            members.Add(new ProjectUser.ProjectUser(Id, userId));
            Update(new UserAssignedToProject(Id, userId));
        }

        private void CheckIfUserIsAlreadyAssigned(Guid userId)
        {
            if (Members.Any(x => x.UserId == userId))
                throw new UserAlreadyAssignedToProject(userId, Id);
        }

        public override void Created()
        {
            Update(new ProjectCreated(Id, Name));
        }
    }
}

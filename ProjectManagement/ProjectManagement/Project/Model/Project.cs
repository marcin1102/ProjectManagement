using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Storage;
using Newtonsoft.Json;
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
            Members = new List<Guid>();
        }

        public string Name { get; private set; }

        public string members { get; private set; }
        public ICollection<Guid> Members
        {
            get => JsonConvert.DeserializeObject<ICollection<Guid>>(members);
            set
            {
                members = JsonConvert.SerializeObject(value);
            }
        }
        public void AssignUser(Guid userId)
        {
            CheckIfUserIsAlreadyAssigned(userId);

            var membersIds = Members.ToList();
            membersIds.Add(userId);
            Members = membersIds;
            Update(new UserAssignedToProject(Id, userId));
        }

        private void CheckIfUserIsAlreadyAssigned(Guid userId)
        {
            if (Members.Any(x => x == userId))
                throw new UserAlreadyAssignedToProject(userId, Id);
        }

        public override void Created()
        {
            Update(new ProjectCreated(Id, Name));
        }
    }
}

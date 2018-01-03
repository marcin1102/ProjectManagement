using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Infrastructure.Storage;
using Newtonsoft.Json;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Contracts.Project.Events;
using ProjectManagement.Contracts.Project.Exceptions;
using ProjectManagement.Services;
using ProjectManagement.User.Repository;

namespace ProjectManagement.Project.Model
{
    public class Project : AggregateRoot
    {
        private Project() { }

        public Project(Guid id, string name) : base(id)
        {
            Name = name;
            Members = new List<Guid>();
            Labels = new List<Label.Label>();
        }

        public string Name { get; private set; }

        private string members { get; set; }
        public ICollection<Guid> Members
        {
            get => JsonConvert.DeserializeObject<ICollection<Guid>>(members);
            set
            {
                members = JsonConvert.SerializeObject(value);
            }
        }

        public ICollection<Label.Label> Labels { get; private set; }

        public override void Created()
        {
            Update(new ProjectCreated(Id, Name));
        }

        public void AssignUser(UserRepository userRepository, AssignUserToProject command)
        {
            CheckIfUserExistsInSystem(userRepository, command.UserToAssignId);
            CheckIfUserIsAlreadyAssigned(command.UserToAssignId);

            var membersIds = Members.ToList();
            membersIds.Add(command.UserToAssignId);
            Members = membersIds;
            Update(new UserAssignedToProject(Id, command.UserToAssignId));
        }

        public void AddLabel(AddLabel command)
        {
            var label = new Label.Label(Guid.NewGuid(), Id, command.Name, command.Description);
            Labels.Add(label);
            Update(new LabelAdded(label.Id, Id, label.Name, label.Description));
            command.CreatedId = label.Id;
        }

        private void CheckIfUserIsAlreadyAssigned(Guid userId)
        {
            if (Members.Any(x => x == userId))
                throw new UserAlreadyAssignedToProject(userId, Id);
        }

        private void CheckIfUserExistsInSystem(UserRepository userRepository, Guid userId)
        {
            var user = Task.Run(() => userRepository.FindAsync(userId)).GetAwaiter().GetResult();
            if (user == null)
                throw new EntityDoesNotExist(userId, nameof(User.Model.Member));
        }
    }
}

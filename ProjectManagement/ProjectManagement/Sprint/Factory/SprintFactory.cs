using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Contracts.Sprint.Commands;
using ProjectManagement.Project.Searchers;
using System;
using System.Threading.Tasks;

namespace ProjectManagement.Sprint.Factory
{
    public interface ISprintFactory
    {
        Task<Model.Sprint> GenerateSprint(CreateSprint command);
    }

    public class SprintFactory : ISprintFactory
    {
        private readonly IProjectSearcher projectSearcher;

        public SprintFactory(IProjectSearcher projectSearcher)
        {
            this.projectSearcher = projectSearcher;
        }

        public async Task<Model.Sprint> GenerateSprint(CreateSprint command)
        {
            if (await projectSearcher.DoesProjectExist(command.ProjectId))
                throw new EntityDoesNotExist(command.ProjectId, nameof(Project.Model.Project));

            var sprint = new Model.Sprint(Guid.NewGuid(), command.ProjectId, command.Name, command.Start, command.End);
            sprint.Created();
            command.CreatedId = sprint.Id;
            return sprint;
        }
    }
}

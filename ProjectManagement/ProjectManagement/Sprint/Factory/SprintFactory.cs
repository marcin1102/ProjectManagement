using ProjectManagement.Infrastructure.Primitives.Exceptions;
using ProjectManagement.Contracts.Sprint.Commands;
using ProjectManagement.Project.Searchers;
using System;
using System.Threading.Tasks;
using ProjectManagement.Services;
using ProjectManagement.Infrastructure.CallContexts;

namespace ProjectManagement.Sprint.Factory
{
    public interface ISprintFactory
    {
        Task<Model.Sprint> GenerateSprint(CreateSprint command);
    }

    public class SprintFactory : ISprintFactory
    {
        private readonly IProjectSearcher projectSearcher;
        private readonly IMembershipService authorizationService;
        private readonly CallContext callContext;

        public SprintFactory(IProjectSearcher projectSearcher, IMembershipService authorizationService, CallContext callContext)
        {
            this.projectSearcher = projectSearcher;
            this.authorizationService = authorizationService;
            this.callContext = callContext;
        }

        public async Task<Model.Sprint> GenerateSprint(CreateSprint command)
        {
            await authorizationService.CheckUserMembership(callContext.UserId, command.ProjectId);

            if (!await projectSearcher.DoesProjectExist(command.ProjectId))
                throw new EntityDoesNotExist(command.ProjectId, nameof(Project.Model.Project));

            var sprint = new Model.Sprint(Guid.NewGuid(), command.ProjectId, command.Name, command.Start, command.End);
            sprint.Created();
            command.CreatedId = sprint.Id;
            return sprint;
        }
    }
}

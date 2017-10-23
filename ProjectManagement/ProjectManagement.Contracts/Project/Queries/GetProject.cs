using System;
using FluentValidation;
using Infrastructure.Message;

namespace ProjectManagement.Contracts.Project.Queries
{
    public class GetProject : IQuery<ProjectResponse>
    {
        public GetProject(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }

    public class ProjectResponse
    {
        public ProjectResponse(Guid id, string name, long version)
        {
            Id = id;
            Name = name;
            Version = version;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public long Version { get; private set; }
    }

    public class GetProjectValidator : AbstractValidator<GetProject>
    {
        public GetProjectValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}

using ProjectManagement.Infrastructure.Primitives.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Contracts.DomainExceptions
{
    public class CannotAddChildIssueWhenParentIssueIsDone<TParent, TChild> : DomainException
    {
        public CannotAddChildIssueWhenParentIssueIsDone(Guid parentId)
            : base("ProjectManagement", $"Cannot add new {typeof(TChild).Name} to {typeof(TParent).Name} with id {parentId} because {typeof(TParent).Name} is done")
        {

        }
    }
}

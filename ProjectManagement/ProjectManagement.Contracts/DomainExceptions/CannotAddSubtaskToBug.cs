using ProjectManagement.Infrastructure.Primitives.Exceptions;

namespace ProjectManagement.Contracts.DomainExceptions
{
    public class CannotAddSubtaskToBug : DomainException
    {
        public CannotAddSubtaskToBug(string domainName) :
            base(domainName, "Bugs cannot have subtasks.")
        {
        }
    }
}

using Infrastructure.Exceptions;

namespace ProjectManagement.Contracts.Exceptions
{
    public class CannotAddSubtaskToBug : DomainException
    {
        public CannotAddSubtaskToBug(string domainName) :
            base(domainName, "Bugs cannot have subtasks.")
        {
        }
    }
}

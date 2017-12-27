using ProjectManagement.Infrastructure.Primitives.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Contracts.Sprint.Exceptions
{
    public class CannotStartSprintWhenAnyOtherIsNotFinished : DomainException
    {
        public CannotStartSprintWhenAnyOtherIsNotFinished(Guid sprintId) :
            base("ProjectManagement", $"Cannot start sprint with id {sprintId} when any other is not finished")
        {
        }
    }
}

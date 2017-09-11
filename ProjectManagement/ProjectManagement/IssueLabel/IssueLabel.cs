using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Storage.EF;

namespace ProjectManagement.IssueLabel
{
    public class IssueLabel : IEntity
    {
        private IssueLabel()
        { }
        public IssueLabel(Guid id, Guid issueId, Guid labelId)
        {
            Id = id;
            IssueId = issueId;
            LabelId = labelId;
        }

        public Guid Id { get; private set; }
        public Guid IssueId { get; private set; }
        public Guid LabelId { get; private set; }
    }
}

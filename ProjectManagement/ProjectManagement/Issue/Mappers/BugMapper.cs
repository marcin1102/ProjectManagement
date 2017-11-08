using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Issue.Mappers
{
    public interface IBugMapper
    {
        Model.Bug ChildBugToBug(Model.ChildBug childBug);
        Model.ChildBug BugToChildBug(Model.Bug bug);
    }

    public class BugMapper : IBugMapper
    {
        public Model.Bug ChildBugToBug(Model.ChildBug childBug)
        {
            return new Model.Bug(childBug.Id, childBug.ProjectId, childBug.Title, childBug.Description, childBug.Status, childBug.ReporterId, childBug.AssigneeId,
                childBug.CreatedAt, childBug.UpdatedAt, childBug.Labels, childBug.Comments);
        }

        public Model.ChildBug BugToChildBug(Model.Bug bug)
        {
            return new Model.ChildBug(bug.Id, bug.ProjectId, bug.Title, bug.Description, bug.Status, bug.ReporterId, bug.AssigneeId,
                bug.CreatedAt, bug.UpdatedAt, bug.Labels, bug.Comments);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Storage.EF.Repository;

namespace ProjectManagement.Label.Repository
{
    public class LabelRepository : Repository<Label>
    {
        public LabelRepository(ProjectManagementContext db) : base(db)
        {
        }
    }
}

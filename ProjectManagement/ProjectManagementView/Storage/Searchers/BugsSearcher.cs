using Microsoft.EntityFrameworkCore;
using ProjectManagementView.Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementView.Storage.Searchers
{
    public interface IBugsSearcher
    {
    }

    public class BugsSearcher : IBugsSearcher
    {
        private readonly ProjectManagementViewContext db;

        public BugsSearcher(ProjectManagementViewContext db)
        {
            this.db = db;
        }
    }
}

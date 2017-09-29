using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagementView.Storage.Models.Abstract;

namespace ProjectManagementView.Storage.Models
{
    public class Nfr : Issue
    {
        public Nfr(Guid id) : base(id)
        {
        }

        public ICollection<Bug> Bugs { get; set; }
    }
}

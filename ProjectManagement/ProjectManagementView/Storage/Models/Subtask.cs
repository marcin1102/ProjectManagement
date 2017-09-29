using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagementView.Storage.Models.Abstract;

namespace ProjectManagementView.Storage.Models
{
    public class Subtask : Issue
    {
        public Subtask(Guid id) : base(id)
        {
        }
    }
}

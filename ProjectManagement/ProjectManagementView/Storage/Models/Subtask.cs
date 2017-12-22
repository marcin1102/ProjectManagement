using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagementView.Storage.Models.Abstract;

namespace ProjectManagementView.Storage.Models
{
    public class Subtask : Issue
    {
        private Subtask() : base() { }


        public Subtask(Guid id) : base(id)
        {
        }
    }
}

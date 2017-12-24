using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagementView.Storage.Models.Abstract;

namespace ProjectManagementView.Storage.Models
{
    public class Task : Issue
    {
        private Task() : base() { }

        public Task(Guid id) : base(id)
        {
            Subtasks = new List<Subtask>();
            Bugs = new List<Bug>();
        }

        public ICollection<Subtask> Subtasks { get; set; }
        public ICollection<Bug> Bugs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Storage.EF;

namespace ProjectManagementView.Storage.Models
{
    public class Label
    {
        public Label(Guid id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}

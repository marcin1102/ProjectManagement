﻿using System;
using System.Collections.Generic;
using System.Text;
using ProjectManagementView.Storage.Models.Abstract;

namespace ProjectManagementView.Storage.Models
{
    public class Bug : Issue
    {
        public Bug(Guid id) : base(id)
        {
        }
    }
}

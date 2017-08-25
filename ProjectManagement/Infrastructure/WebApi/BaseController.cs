using System;
using System.Collections.Generic;
using System.Text;
using Infrastructure.Message.CommandQueryBus;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.WebApi
{
    public class BaseController : Controller
    {
        private readonly ICommandQueryBus commandQueryBus;

        public BaseController(ICommandQueryBus commandQueryBus)
        {
            this.commandQueryBus = commandQueryBus;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManagement.Contracts.Project.Commands;
using ProjectManagement.Project.Model;
using ProjectManagement.Services;

namespace ProjectManagement.Project.Factory
{
    public interface IProjectFactory
    {
        Task<Model.Project> GenerateProject(CreateProject command);
    }
    public class ProjectFactory : IProjectFactory
    {
        private readonly IAuthorizationService authorizationService;

        public ProjectFactory(IAuthorizationService authorizationService)
        {
            this.authorizationService = authorizationService;
        }

        public async Task<Model.Project> GenerateProject(CreateProject command)
        {
            await authorizationService.CheckUserRole(command.AdminId, nameof(CreateProject));

            var project = new Model.Project(Guid.NewGuid(), command.Name);
            project.Created();
            command.CreatedId = project.Id;
            return project;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Infrastructure.Bootstrap;
using Infrastructure.Settings;
using Infrastructure.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Converters;
using ProjectManagement.Contracts.Project.Commands;
using Swashbuckle.AspNetCore.Swagger;
using UserManagement;
using UserManagement.Contracts.User.Commands;
using WebApi.Bootstrap;

namespace ProjectManagement.WebApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; private set; }
        public ILoggerFactory LoggerFactory { get; private set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options => options.AddFilters())
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CreateUser>().RegisterValidatorsFromAssemblyContaining<CreateProject>())
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter());
                });

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("api", new Info { Title = "DDD-app" });
            });

            services.RegisterInfrastructureComponents(Configuration);
            services.RegisterAppModules(Configuration, LoggerFactory);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            LoggerFactory = loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseMvc();
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                    {
                        x.RoutePrefix = "swagger/ui";
                        x.SwaggerEndpoint("/swagger/api/swagger.json", "Docs");
                    });
                app.UsePathBase("/swagger/ui");
            }
        }
    }
}

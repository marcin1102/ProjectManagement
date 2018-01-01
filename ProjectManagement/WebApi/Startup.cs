using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using ProjectManagement.Infrastructure.Bootstrap;
using ProjectManagement.Infrastructure.Settings;
using ProjectManagement.Infrastructure.WebApi;
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
using WebApi.Middlewares;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;

namespace ProjectManagement.WebApi
{
    public class Startup
    {
        public IContainer ApplicationContainer { get; private set; }
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

        public IServiceProvider ConfigureServices(IServiceCollection services)
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
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "WebApi.xml");
                x.IncludeXmlComments(filePath);
            });

            var builder = new ContainerBuilder();

            builder.RegisterInfrastructureComponents(Configuration);
            builder.RegisterAppModules(Configuration, LoggerFactory);

            builder
                .RegisterType<AuthMiddleware>()
                .InstancePerLifetimeScope();

            builder.Populate(services);
            ApplicationContainer = builder.Build();

            ApplicationContainer.UseAppModules();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            LoggerFactory = loggerFactory.AddConsole();

            app.UseMiddleware<AuthMiddleware>();

            app.UseCors(conf => 
                    conf
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader())
                .UseMvc();
                
            app.UseSwagger();
            app.UseSwaggerUI(x =>
                {
                    x.RoutePrefix = "swagger/ui";
                    x.SwaggerEndpoint("/swagger/api/swagger.json", "Docs");
                });
            app.UsePathBase("/swagger/ui");
                      

            appLifetime.ApplicationStopped.Register(() => ApplicationContainer.Dispose());
        }
    }
}

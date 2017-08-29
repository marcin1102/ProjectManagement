using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Infrastructure.Bootstrap;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using WebApi.Bootstrap;

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
            services.AddMvc();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

            var builder = new ContainerBuilder();

            builder.RegisterInfrastructureComponents(Configuration);
            builder.RegisterAppModules(Configuration, LoggerFactory);

            builder.Populate(services);
            ApplicationContainer = builder.Build();
            return new AutofacServiceProvider(ApplicationContainer);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime appLifetime)
        {
            LoggerFactory = loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMvc();
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                    {
                        x.RoutePrefix = "swagger/ui";
                        x.SwaggerEndpoint("/swagger/v1/swagger.json", "Docs");
                    });
            }

            appLifetime.ApplicationStopped.Register(() => this.ApplicationContainer.Dispose());
        }
    }
}

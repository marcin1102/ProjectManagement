using System;
using System.Reflection;
using ProjectManagement.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace ProjectManagement.Infrastructure.Storage.EF
{
    public class DbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
        where TContext : DbContext
    {
        protected readonly GlobalSettings globalSettings;
        public DbContextFactory()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")?.ToLower() ?? "development";

            var builder = new ConfigurationBuilder()
                                  .SetBasePath(AppContext.BaseDirectory)
                                  .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                                  .AddJsonFile($"appsettings.{environment}.json", optional: false)
                                  .AddEnvironmentVariables();

            var configuration = builder.Build();
            globalSettings = configuration.GetSection("GlobalSettings").Get<GlobalSettings>();
        }

        public TContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            var assembly = typeof(TContext).GetTypeInfo().Assembly;

            builder.UseNpgsql(globalSettings.ConnectionString, x => x.MigrationsAssembly(assembly.FullName));
            return Activator.CreateInstance(typeof(TContext), builder.Options) as TContext;
        }
    }
}

using Autofac;
using ProjectManagement;

namespace WebApi.Bootstrap
{
    public static class BootstrapExtensions
    {
        public static void RegisterAppModules(this ContainerBuilder builder)
        {
            new ProjectManagementBootstrap(builder);
        }
    }
}

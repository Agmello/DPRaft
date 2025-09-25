using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleFrontend
{
    internal static class DependencyInjection
    {
        internal static IHost AddConsoleFrontend()
        {

            var hostBuilder = Host.CreateDefaultBuilder();

            hostBuilder.ConfigureServices(AddModules);
            var builder = hostBuilder.Build();
           
            return builder;
        }
        private static void AddModules(this IServiceCollection services)
        {
            // Register services related to various modules here
            // Example:
            // services.AddSingleton<IModuleService, ModuleService>();
            Core.Infrastructure.DependencyInjection.AddCoreInfrastructure(services);
        }
    }
}

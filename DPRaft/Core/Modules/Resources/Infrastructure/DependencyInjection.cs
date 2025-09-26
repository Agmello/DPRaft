using Core.Modules.Resources.Application.Contracts;
using Core.Modules.Resources.Application.Services;
using Core.Modules.Resources.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Resources.Infrastructure
{
    internal static class ResourceDependencyInjection
    {
        internal static void AddModule(this IServiceCollection services)
        {
            // Register services related to the Resources module here
            // Example:
            // services.AddSingleton<IResourceService, ResourceService>();
            services.AddSingleton<IResourceRepository, ResourceRepository>();
            services.AddSingleton<IYieldComposite, YieldComposite>();
            services.AddSingleton<IResourceMediator, ResourceMediator>();

        }
    }
}

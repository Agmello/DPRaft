using Core.Modules.Buildings.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Infrastructure
{
    internal static class BuildingsDependencyInjection
    {
        internal static void AddModule(this IServiceCollection services)
        {
            // Register services related to the Buildings module here
            // Example:
            // services.AddSingleton<IBuildingService, BuildingService>();
            services.AddSingleton<ITileBuildingFactory, TileBuildingFactory>();
            services.AddSingleton<IBuildingRepository, BuildingRepository>();
        }
    }
}

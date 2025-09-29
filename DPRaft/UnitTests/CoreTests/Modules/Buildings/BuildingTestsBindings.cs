using Core.Modules.Buildings.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Modules.Buildings.Infrastructure;
using Core.Infrastructure;
using Core.Modules.Buildings.Domain;

namespace UnitTests.CoreTests.Modules.Buildings
{
    internal class BuildingTestsBindings
    {
        public static void Configure(IServiceCollection services)
        {
            // This method is intentionally left blank.
            // Its purpose is to ensure that the compiler includes this file in the build.
            DependencyInjection.AddTestInfrastructure(services, "building", true);
        }
    }
}

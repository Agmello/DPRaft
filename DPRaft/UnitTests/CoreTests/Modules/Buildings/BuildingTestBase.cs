using Core.Modules.Buildings.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace UnitTests.CoreTests.Modules.Buildings
{
    public abstract class BuildingTestBase : TestBase
    {
        protected virtual bool InitBuildings => false;
        protected Building[] AllBuilding { get; set; }
        protected BuildingTestBase() : base()
        {
            if(InitBuildings)
                AllBuilding =  typeof(Building)
                     .Assembly.GetTypes()
                     .Where(t => t.IsSubclassOf(typeof(Building)) && !t.IsAbstract)
                     .Select(t => (Building)Activator.CreateInstance(t)).ToArray();
        }

        protected override sealed void SetupData()
        {
            SetupBuildingData();
        }
        protected override void SetupServices(IServiceCollection services)
        {
            Core.Infrastructure.DependencyInjection.AddTestInfrastructure(services, "Buildings", true);
        }
        protected abstract void SetupBuildingData();
    }
}

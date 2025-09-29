using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

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
                BuildingsDependencyInjection.AddModule(services);
        }
        protected abstract void SetupBuildingData();
    }
}

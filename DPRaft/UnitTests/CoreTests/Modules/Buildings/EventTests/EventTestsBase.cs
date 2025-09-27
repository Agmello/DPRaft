using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Buildings.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CoreTests.Modules.Buildings.EventTests
{

    public class BuildingEventTestsBase : TestBase
    {
        List<string> TriggeredMethods = new List<string>();

        [Fact]
        public void EventTriggerBase()
        {
            // Arrange
            var observer = ServiceProvider.GetService<IEventObserver>();
            var factory = ServiceProvider.GetService<Core.Modules.Buildings.Domain.Contracts.ITileBuildingFactory>();
            var repo = ServiceProvider.GetService<IBuildingRepository>();

            observer.SubscribeSafe<BuildingChangedEvent>(BuildingEventRecieved);
            observer.SubscribeSafe<YieldBuildingEvent>(ProductionBuildingEventRecieved);
            // Act
            var building = factory.Create("Farm");
            repo.AddBuilding(new Core.Modules.Tiles.Domain.Tile(), building);
            // Assert
            Assert.Contains("BuildingEventRecieved", TriggeredMethods);
            Assert.Contains("ProductionBuildingEventRecieved", TriggeredMethods);
        }

        private void BuildingEventRecieved(BuildingChangedEvent @event)
        {
            TriggeredMethod();
        }
        private void ProductionBuildingEventRecieved(YieldBuildingEvent @event)
        {
            TriggeredMethod();
        }
        private void TriggeredMethod([CallerMemberName] string methodName = null)
        {
            TriggeredMethods.Add(methodName);
        }


        protected override void SetupData()
        {

        }

        protected override void SetupServices(IServiceCollection services)
        {
            Core.Infrastructure.DependencyInjection.AddTestInfrastructure(services, "Buildings", true);
        }
    }
}

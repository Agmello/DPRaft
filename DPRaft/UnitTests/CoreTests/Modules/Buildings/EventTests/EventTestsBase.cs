using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Buildings.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.CompilerServices;

namespace UnitTests.CoreTests.Modules.Buildings.EventTests
{

    public class BuildingEventTestsBase : TestBase
    {
        List<string> TriggeredMethods = new List<string>();
        IEventObserver m_observer;

        [Fact]
        public void EventTriggerBase()
        {
            // Arrange
            var factory = ServiceProvider.GetService<Core.Modules.Buildings.Domain.Contracts.ITileBuildingFactory>();
            var repo = ServiceProvider.GetService<IBuildingRepository>();

            m_observer.SubscribeSafe<BuildingChangedEvent>(BuildingEventRecieved);
            m_observer.SubscribeSafe<YieldBuildingEvent>(ProductionBuildingEventRecieved);
            // Act
            var building = factory.Create("Farm");
            repo.AddBuilding(new Core.Modules.Tiles.Domain.Tile(), building);
            // Assert
            Assert.Contains("BuildingEventRecieved", TriggeredMethods);
            Assert.Contains("ProductionBuildingEventRecieved", TriggeredMethods);
        }

        [Fact]
        public void EventTriggeredByTemplate()
        {
            m_observer.SubscribeSafe<YieldBuildingEvent>(ProductionBuildingEventRecieved);
            var publisher = ServiceProvider.GetService<IEventPublisher>();

            publisher.Publish(new YieldBuildingEvent("Farm"));
            Assert.Contains("ProductionBuildingEventRecieved", TriggeredMethods);
        }
        [Fact]
        public void EventTriggeredByType()
        {
            m_observer.SubscribeSafe<YieldBuildingEvent>(ProductionBuildingEventRecieved);
            var publisher = ServiceProvider.GetService<IEventPublisher>();

            publisher.Publish(typeof(YieldBuildingEvent), new YieldBuildingEvent("Farm"));
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
            m_observer = ServiceProvider.GetService<IEventObserver>();
        }

        protected override void SetupServices(IServiceCollection services)
        {
            Core.Infrastructure.DependencyInjection.AddTestInfrastructure(services, "Buildings", true);
        }
    }
}

using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Application.Services;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Buildings.Domain.Events;
using Core.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CoreTests.Modules.Buildings.UpgradeTests
{
    public class UpgradeTestsBase : BuildingTestBase
    {
        UpgradeComposite m_upgradeComposite;
        ITileBuildingFactory m_factory;
        IEventObserver m_observer;
        [Fact]
        public void VerifyUpgradeOrder()
        {
            var building = m_factory.Create("Farm");
            var upgrade = building.AvailableUpgrades.FirstOrDefault();
            var upgradeBuilding = m_factory.Create(upgrade.Name);

            var status = m_upgradeComposite.Enqueue(new Core.Modules.Tiles.Domain.Tile(), building, upgradeBuilding, upgrade.TicksToComplete);

            Assert.Equal(UpgradeStatus.Upgrading, status.Status);
            for(int i = 0; i < upgrade.TicksToComplete-1; i++)
            {
                m_upgradeComposite.ExecuteTick();
                Assert.Equal(status.TicksLeft, upgrade.TicksToComplete - (i + 1));
            }
            m_upgradeComposite.ExecuteTick();
            Assert.Equal(UpgradeStatus.Done, status.Status);
        }



        [Fact]
        public void VerifyUpgradeEvents()
        {
            IEvent lastEvent = null;
            var building = m_factory.Create("Farm");
            var upgrade = building.AvailableUpgrades.FirstOrDefault();
            var upgradeBuilding = m_factory.Create(upgrade.Name);
            m_observer.SubscribeSafe<BuildingChangedEvent>(e => lastEvent = e);
            
            var status = m_upgradeComposite.Enqueue(new Core.Modules.Tiles.Domain.Tile(), building, upgradeBuilding, upgrade.TicksToComplete);
            Assert.True(VerifyEvent(lastEvent, status));
            while(status.Status == UpgradeStatus.Upgrading)
            {
                m_upgradeComposite.ExecuteTick();
            }
            Assert.True(VerifyEvent(lastEvent, status));

        }
        bool VerifyEvent(IEvent @event, UpgradeInformation info)
        {
            var e = @event as BuildingChangedEvent;
            if(e.Building != info.From) 
                return false;
            if(e.Tile != info.Tile)
                return false;
            if(e.NewBuilding != info.To)
                return false;
            var status =e.Change switch
            {
                ChangeType.Upgrading => info.Status == UpgradeStatus.Upgrading,
                ChangeType.Upgraded => info.Status == UpgradeStatus.Done,
                ChangeType.UprgadeStopped => info.Status == UpgradeStatus.Aborted,
                _ => false
            };
            return status;
        }


        protected override void SetupBuildingData()
        {
            // No building data needed
            m_upgradeComposite = ServiceProvider.GetService<UpgradeComposite>();
            m_factory = ServiceProvider.GetService<ITileBuildingFactory>();
            m_observer = ServiceProvider.GetService<IEventObserver>();
        }
        protected override void SetupServices(IServiceCollection services)
        {
            SetupEventHandlers(services);
            base.SetupServices(services);
        }
    }
}

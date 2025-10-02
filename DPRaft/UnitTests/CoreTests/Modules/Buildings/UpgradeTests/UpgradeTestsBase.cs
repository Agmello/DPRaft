using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Application.Contracts;
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
using NSubstitute;
using Core.Modules.Buildings.Domain;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Core.Modules.Resources.Domain.Contracts;
using Core.Modules.Buildings.Infrastructure;

namespace UnitTests.CoreTests.Modules.Buildings.UpgradeTests
{
    public class UpgradeTestsBase : BuildingTestBase
    {
        protected IUpgradeManager m_upgradeManager;

        protected ITileBuildingFactory m_factory;
        IEventObserver m_observer;


        

        


        /*[Fact]
        public void VerifyUpgradeEvents()
        {
            IEvent lastEvent = null;
            var building = m_factory.Create("Farm");
            var upgrade = building.AvailableUpgrades.FirstOrDefault();
            var upgradeBuilding = m_factory.Create(upgrade.Name);
            m_observer.SubscribeSafe<BuildingChangedEvent>(e => lastEvent = e);
            
            var status = m_upgradeManager.Enqueue(new Core.Modules.Tiles.Domain.Tile(), building, upgradeBuilding, upgrade.TicksToComplete);
            Assert.True(VerifyEvent(lastEvent, status));
            while(status.Status == UpgradeStatus.Upgrading)
            {
                m_upgradeManager.ExecuteTick();
            }
            Assert.True(VerifyEvent(lastEvent, status));

        }*/
        bool VerifyEvent(IEvent @event, UpgradeOperation info)
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
                ChangeType.UpgradeStopped => info.Status == UpgradeStatus.Aborted,
                _ => false
            };
            return status;
        }


        protected override void SetupBuildingData()
        {
            // No building data needed
            m_upgradeManager = ServiceProvider.GetService<IUpgradeManager>();
            m_factory = ServiceProvider.GetService<ITileBuildingFactory>();
        }
        protected override void SetupServices(IServiceCollection services)
        {
            services.AddSingleton<ITileBuildingFactory, TileBuildingFactory>();
            services.AddSingleton<IUpgradeManager, UpgradeManager>();

            //SetupEventHandlers(services);
            //base.SetupServices(services);
            //services.Replace(ServiceDescriptor.Singleton<IBuildingRepository,Substitute.For<IBuildingRepository>());
            //services.AddSingleton<IBuildingRepository>(Substitute.For<IBuildingRepository>());
            //services.AddSingleton(Substitute.For<IResourceRepository>());
        }
    }
}

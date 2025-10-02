using Core.Modules.Buildings.Application.Contracts;
using Core.Modules.Buildings.Application.Services;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Buildings.Infrastructure;
using Core.Modules.Resources.Domain.Contracts;
using Core.Modules.Resources.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CoreTests.Modules.Buildings.UpgradeTests
{
    public class UpgradeWithRepository : UpgradeTestsBase
    {
        IBuildingRepository m_repository;

        [Fact]
        public void StartFromRepository()
        {
            var building = m_factory.Create("Farm");
            m_repository.AddBuilding(new Core.Modules.Tiles.Domain.Tile(), building);

            var operation = m_repository.StartUpgrade(building, 1);
            var ticks = operation.TicksLeft;
            Assert.Equal(UpgradeStatus.Upgrading, operation.Status);
            for (int i = 0; i < ticks - 1; i++)
            {
                m_upgradeManager.ProcessUpgrades();
                Assert.Equal(operation.TicksLeft, ticks - (i + 1));
            }
            m_upgradeManager.ProcessUpgrades();
            Assert.Equal(UpgradeStatus.Done, operation.Status);
        }
        [Fact]
        public void PauseFromRespository()
        {
            var building = m_factory.Create("Farm");
            m_repository.AddBuilding(new Core.Modules.Tiles.Domain.Tile(), building);

            var operation = m_repository.StartUpgrade(building, 1);
            var ticks = operation.TicksLeft;

            Assert.Equal(UpgradeStatus.Upgrading, operation.Status);
            m_upgradeManager.ProcessUpgrades();
            Assert.Equal(ticks - 1, operation.TicksLeft);
            m_repository.PauseUpgrade(operation.From);
            Assert.Equal(UpgradeStatus.Stopped, operation.Status);
            m_upgradeManager.ProcessUpgrades();
            Assert.Equal(ticks - 1, operation.TicksLeft);
            m_repository.ResumeUpgrade(operation.From);
            Assert.Equal(UpgradeStatus.Upgrading, operation.Status);
            m_upgradeManager.ProcessUpgrades();
            Assert.Equal(ticks - 2, operation.TicksLeft);
        }

        [Fact]
        public void AbortFromRepository()
        {
            var building = m_factory.Create("Farm");
            m_repository.AddBuilding(new Core.Modules.Tiles.Domain.Tile(), building);

            var operation = m_repository.StartUpgrade(building, 1);
            var ticks = operation.TicksLeft;
            Assert.Equal(UpgradeStatus.Upgrading, operation.Status);
            m_upgradeManager.ProcessUpgrades();
            Assert.Equal(ticks - 1, operation.TicksLeft);

            m_repository.AbortUpgrade(operation.From);
            Assert.Equal(UpgradeStatus.Aborted, operation.Status);
            m_upgradeManager.ProcessUpgrades();
            Assert.Equal(0, operation.TicksLeft);
        }
        [Fact]
        public void FailedFromRepository()
        {
            var resourceRepository = ServiceProvider.GetRequiredService<IResourceRepository>();
            resourceRepository.Get(Arg.Any<string>()).Returns(1d);

            var building = m_factory.Create("Farm");
            m_repository.AddBuilding(new Core.Modules.Tiles.Domain.Tile(), building);

            var operation = m_repository.StartUpgrade(building, 1);
            var ticks = operation.TicksLeft;
            Assert.Equal(UpgradeStatus.Failed, operation.Status);
            m_upgradeManager.ProcessUpgrades();
            Assert.Equal(ticks, operation.TicksLeft);
        }


        protected override void SetupBuildingData()
        {
            base.SetupBuildingData();
            m_repository = ServiceProvider.GetRequiredService<IBuildingRepository>();
            var resourceRepository = ServiceProvider.GetRequiredService<IResourceRepository>();
            resourceRepository.Get(Arg.Any<string>()).Returns(100d);
        }
        protected override void SetupServices(IServiceCollection services)
        {
            SetupEventHandlers(services);
            base.SetupServices(services);
            services.AddSingleton<IUpgradeService, UpgradeService>();
            services.AddSingleton<IBuildingRepository, BuildingRepository>();
            services.AddSingleton(Substitute.For<IResourceRepository>());
        }
    }
}

using Core.Modules.Buildings.Application.Contracts;
using Core.Modules.Buildings.Application.Services;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Domain.Contracts;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CoreTests.Modules.Buildings.UpgradeTests
{
    public class UpgradeWithService : UpgradeTestsBase
    {
        IUpgradeService m_upgradeService;
        IResourceRepository m_resourceRepository;

        [Fact]
        public void StartFromService()
        {
            m_resourceRepository.Get(Arg.Any<string>()).Returns(100d);

            var building = m_factory.Create("Farm");
            var operation = m_upgradeService.StartUpgrade(new Core.Modules.Tiles.Domain.Tile(),building, 1);
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
        protected override void SetupBuildingData()
        {
            base.SetupBuildingData();
            m_upgradeService = ServiceProvider.GetRequiredService<IUpgradeService>();
            m_resourceRepository = ServiceProvider.GetRequiredService<IResourceRepository>();
        }
        protected override void SetupServices(IServiceCollection services)
        {
            base.SetupServices(services);
            services.AddSingleton<IUpgradeService, UpgradeService>();
            services.AddSingleton(Substitute.For<IResourceRepository>());
        }
    }
}

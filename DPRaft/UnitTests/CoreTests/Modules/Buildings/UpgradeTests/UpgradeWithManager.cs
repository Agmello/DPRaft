using Core.Modules.Buildings.Application.Services;
using Information.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CoreTests.Modules.Buildings.UpgradeTests
{
    public class UpgradeWithManager : UpgradeTestsBase
    {
        [Fact]
        public void VerifyUpgradeOrder()
        {
            var building = m_factory.Create("Farm");
            var upgrade = building.AvailableUpgrades.FirstOrDefault();
            var upgradeBuilding = m_factory.Create(upgrade.Name);

            var operation = new UpgradeOperation(new Core.Modules.Tiles.Domain.Tile(), building, upgradeBuilding, upgrade.TicksToComplete, upgrade.Costs);
            m_upgradeManager.StartUpgrade(operation);

            Assert.Equal(UpgradeStatus.Upgrading, operation.Status);
            for (int i = 0; i < upgrade.TicksToComplete - 1; i++)
            {
                m_upgradeManager.ProcessUpgrades();
                Assert.Equal(operation.TicksLeft, upgrade.TicksToComplete - (i + 1));
            }
            m_upgradeManager.ProcessUpgrades();
            Assert.Equal(UpgradeStatus.Done, operation.Status);
        }
    }
}

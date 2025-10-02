using Core.Modules.Buildings.Application.Services;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Events;
using Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CoreTests.Modules.Buildings.UpgradeTests.UpgradeEventTests
{
    public class CorrectUpgradingEvent : UpgradeEventTestsBase
    {
        ChangeType ExpectedChange;
        [Fact]
        public void EventsRecieved()
        {
            Run((b) =>
            {
                int ticks = b.TicksLeft;
                for (int i = 0; i < ticks; i++)
                    m_upgradeManager.ProcessUpgrades();
            }, [ChangeType.Added, ChangeType.Upgrading, ChangeType.Upgraded]);
        }

        [Fact]
        public void AbortEventRecieved()
        {
            Run((b) => m_repository.AbortUpgrade(b.From), [ChangeType.Added, ChangeType.Upgrading, ChangeType.UpgradeStopped]);
        }
        [Fact]
        public void PauseEventRecieved()
        {
            Run((b) => m_repository.PauseUpgrade(b.From), [ChangeType.Added, ChangeType.Upgrading, ChangeType.UpgradeStopped]);
        }

        protected void Run(Action<UpgradeOperation> action,IEnumerable<ChangeType> expectedChanges)
        {
            var building = m_factory.Create("Farm");
            m_repository.AddBuilding(new Core.Modules.Tiles.Domain.Tile(), building);
            var operation = m_repository.StartUpgrade(building, 1);
            var ticks = operation.TicksLeft;

            action.Invoke(operation);

            foreach (var change in expectedChanges)
            {
                ExpectedChange = change;
                CheckEvent();
            }
            AssertNoMoreEvents();
        }

        protected override void EventCheckCallback(IEvent @event)
        {
            if (@event is BuildingChangedEvent e)
            {
                Assert.Equal(ExpectedChange, e.Change);
            }
            else
            {
                Assert.True(false, "Event is not of expected type");
            }
        }
    }
}

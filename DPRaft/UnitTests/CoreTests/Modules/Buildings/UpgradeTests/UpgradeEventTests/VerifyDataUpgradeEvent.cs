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
    public class VerifyDataUpgradeEvent : UpgradeEventTestsBase
    {
        Building Building;
        ChangeType ChangeType;
        Building NewBuilding;

        [Fact]
        public void VerifyEventData()
        {
            /*var building = m_factory.Create("Farm");
            m_repository.AddBuilding(new Core.Modules.Tiles.Domain.Tile(), building);
            AddDataAndCheckEvent(building, ChangeType.Added, null);
            var operation = m_repository.StartUpgrade(building, 1);
            AddDataAndCheckEvent(operation.From, ChangeType.Upgrading, operation.To);
            var ticks = operation.TicksLeft;
            for (int i = 0; i < ticks; i++)
                m_upgradeManager.ProcessUpgrades();
            AddDataAndCheckEvent(operation.From, ChangeType.Upgraded, operation.To);
            AssertNoMoreEvents();*/
            Run((b) =>
            {
                var ticks = b.TicksLeft;
                for (int i = 0; i < ticks; i++)
                    m_upgradeManager.ProcessUpgrades();
                AddDataAndCheckEvent(b.From, ChangeType.Upgraded, b.To);
            });
        }

        [Fact]
        public void VerifyAbortEventData()
        {
            /*var building = m_factory.Create("Farm");
            m_repository.AddBuilding(new Core.Modules.Tiles.Domain.Tile(), building);
            AddDataAndCheckEvent(building, ChangeType.Added, null);
            var operation = m_repository.StartUpgrade(building, 1);
            AddDataAndCheckEvent(operation.From, ChangeType.Upgrading, operation.To);
            m_repository.AbortUpgrade(operation.From);
            AddDataAndCheckEvent(operation.From, ChangeType.UpgradeStopped, null);
            AssertNoMoreEvents();*/
            Run((b) =>
            {
                m_repository.AbortUpgrade(b.From);
                AddDataAndCheckEvent(b.From, ChangeType.UpgradeStopped, null);
            });
        }
        [Fact]
        public void VerifyPauseEventData()
        {
            /*var building = m_factory.Create("Farm");
            m_repository.AddBuilding(new Core.Modules.Tiles.Domain.Tile(), building);
            AddDataAndCheckEvent(building, ChangeType.Added, null);
            var operation = m_repository.StartUpgrade(building, 1);
            AddDataAndCheckEvent(operation.From, ChangeType.Upgrading, operation.To);
            m_repository.PauseUpgrade(operation.From);
            AddDataAndCheckEvent(operation.From, ChangeType.UpgradeStopped, operation.To);
            AssertNoMoreEvents();*/
            Run((b) =>
            {
                m_repository.PauseUpgrade(b.From);
                AddDataAndCheckEvent(b.From, ChangeType.UpgradeStopped, b.To);
            });
        }
        private void Run(Action<UpgradeOperation> action)
        {
            var building = m_factory.Create("Farm");
            m_repository.AddBuilding(new Core.Modules.Tiles.Domain.Tile(), building);
            AddDataAndCheckEvent(building, ChangeType.Added, null);
            var operation = m_repository.StartUpgrade(building, 1);
            AddDataAndCheckEvent(operation.From, ChangeType.Upgrading, operation.To);
            action.Invoke(operation);
            AssertNoMoreEvents();
        }

        void AddDataAndCheckEvent(Building building, ChangeType type, Building newBuilding)
        {
            SetData(building, type, newBuilding);
            CheckEvent();
        }
        void SetData(Building building, ChangeType type, Building newBuilding)
        {
            Building = building;
            ChangeType = type;
            NewBuilding = newBuilding;
        }
        protected override void EventCheckCallback(IEvent @event)
        {
            if(@event is not BuildingChangedEvent e)
                throw new Exception("Event is not BuildingChangedEvent");
            Assert.Equal(Building, e.Building);
            Assert.Equal(ChangeType, e.Change);
            Assert.Equal(NewBuilding, e.NewBuilding);
        }
    }
}

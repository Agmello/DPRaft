using Core.Modules.Buildings.Application.Services;
using Core.Modules.Buildings.Domain.Events;
using Core.Modules.Tiles.Domain;
using Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CoreTests.Modules.Buildings.ConstructionTests.ConstructionEventTests
{
    public class VerifyConstructionEventRecievedTests : ConstructionEventTestBase
    {
        ChangeType ExpectedChange;
        [Fact]
        public void EventsRecieved()
        {
            Run((tile) => { m_repository.DestroyBuilding(tile); }, new List<ChangeType> { ChangeType.Added, ChangeType.Removed });
        }
        [Fact]
        public void DestroySpotEventsRecieved()
        {
            Run((tile) => { m_repository.RemoveBuildingSpot(tile); }, new List<ChangeType> { ChangeType.Added, ChangeType.Removed });
        }
        [Fact]
        public void VerifyEventData()
        {
            Run((tile) => 
            {
                var alotment = m_factory.Create("Alotment");
                m_repository.SetBuilding(tile, alotment);
                m_repository.DestroyBuilding(tile);
                m_repository.SetBuilding(tile, alotment);
            }, new List<ChangeType> { ChangeType.Added, ChangeType.Upgraded, ChangeType.Removed, ChangeType.Added });
        }

        protected void Run(Action<Tile> action, IEnumerable<ChangeType> expectedChanges)
        {
            m_factory.Create("Farm");
            var tile = new Core.Modules.Tiles.Domain.Tile();
            m_repository.AddBuilding(tile, m_factory.Create("Farm"));

            action.Invoke(tile);

            foreach (var change in expectedChanges)
            {
                ExpectedChange = change;
                CheckEvent();
            }
            AssertNoMoreEvents();
        }

        protected override void EventCheckCallback(IEvent @event)
        {
            if(@event is not BuildingChangedEvent e)
                throw new Exception("Event is not BuildingChangedEvent");
            Assert.Equal(ExpectedChange, e.Change);
            
        }
    }
}

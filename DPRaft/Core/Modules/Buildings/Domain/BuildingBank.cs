using Core.BuildingBlocks.Messaging;
using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Buildings.Domain.Events;
using Core.Modules.Game.Domain.Banks;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Domain
{
    internal class BuildingBank : BankBase
    {
        Dictionary<Tile, Building> m_bank = new();
        ITileBuildingFactory m_factory;

        internal BuildingBank(
            Guid key,
            IEventPublisher publisher,
            ITileBuildingFactory tileBuildingFactory) : base(key, publisher)
        {
            m_factory = tileBuildingFactory ?? throw new ArgumentNullException(nameof(tileBuildingFactory));
        }

        internal IEnumerable<(Tile, Building)> GetAll(Guid key)
        {
            return hasAccess(key) ? m_bank.Select(x => (x.Key,x.Value)).ToList() :
                throw new ArgumentNullException(nameof(key));
        }
        internal IEnumerable<Building> GetAllBuildings(Guid key)
        {
            return hasAccess(key) ? m_bank.Select(x => x.Value).ToList() :
                throw new ArgumentNullException(nameof(key));
        }
        internal Building AddBuilding(Guid key, Tile tile, Building building)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            m_bank[tile] = building;
            NotifyBuildingChange(building, tile, ChangeType.Added);
            return m_bank[tile];
        }
        internal Building RemoveBuilding(Guid key, Tile tile)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            if (m_bank.ContainsKey(tile))
            {
                var building = m_bank[tile];
                m_bank[tile] = m_factory.CreateEmptyBuilding();
                NotifyBuildingChange(building, tile, ChangeType.Removed);

                return building;
            }
            return null;
        }
        internal Building UpgradeBuilding(Guid key, Tile tile, Building building)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            if (m_bank.ContainsKey(tile))
            {
                var oldBuilding = m_bank[tile];
                m_bank[tile] = building;
                NotifyBuildingChange(building, tile, ChangeType.Updated);
                return building;
            }
            return null;
        }
        internal void RemoveBuildingSpot(Guid key, Tile tile)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            m_bank.Remove(tile);
        }

        private void NotifyBuildingChange(Building building, Tile tile, ChangeType type)
        {
            if (building == null)
                throw new ArgumentNullException(nameof(building));
            if (tile == null)
                throw new ArgumentNullException(nameof(tile));
            m_eventPublisher.Publish(new BuildingChangedEvent(tile, building, type));
        }
    }
}

using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Application.Contracts;
using Core.Modules.Buildings.Application.Services;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Buildings.Domain.Events;
using Core.Modules.Buildings.Infrastructure;
using Core.Modules.Game.Domain.Banks;
using Core.Modules.Tiles.Domain;

namespace Core.Modules.Buildings.Domain
{
    internal class BuildingBank : BankBase
    {
        Dictionary<Tile, Building> m_bank = new();
        ITileBuildingFactory m_factory;
        IUpgradeService m_upgradeService;

        internal BuildingBank(
            Guid key,
            IEventPublisher publisher,
            IUpgradeManager upgradeManager,
            IUpgradeService service,
            ITileBuildingFactory tileBuildingFactory) : base(key, publisher)
        {
            m_factory = tileBuildingFactory ?? throw new ArgumentNullException(nameof(tileBuildingFactory));
            m_upgradeService = service ?? throw new ArgumentNullException(nameof(service));
            (upgradeManager as UpgradeManager).OnUpgradeComplete += UpgradeComplete;
        }

        internal IEnumerable<(Tile, Building)> GetAll(Guid key)
        {
            return hasAccess(key) ? m_bank.Select(x => (x.Key, x.Value)).ToList() :
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
            if (m_bank.TryGetValue(tile,out var building))
            {
                m_bank[tile] = m_factory.CreateEmptyBuilding();
                NotifyBuildingChange(building, tile, ChangeType.Removed);
                return building;
            }
            return null;
        }
        internal Building SetBuilding(Guid key, Tile tile, Building building)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            if(m_bank.TryGetValue(tile, out var oldBuilding))
            {
                m_bank[tile] = building;
                var changeType = oldBuilding.Name == "Empty" ? ChangeType.Added : ChangeType.Upgraded;
                NotifyBuildingChange(oldBuilding, tile, changeType, building);
                return building;
            }
            return null;
        }
        internal Building GetBuilding(Guid key, Tile tile)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            if (m_bank.TryGetValue(tile, out var building))
            {
                return building;
            }
            return null;
        }
        internal void AddBuildingSpot(Guid key, Tile tile)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            var building = m_factory.CreateEmptyBuilding();
            m_bank[tile] = building;
            NotifyBuildingChange(building, tile, ChangeType.Added);
        }
        internal void RemoveBuildingSpot(Guid key, Tile tile)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            if (m_bank.TryGetValue(tile, out var building))
                NotifyBuildingChange(building, tile, ChangeType.Removed);
            m_bank.Remove(tile);
        }

        internal Tile GetTile(Guid key, Building building)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            var item = m_bank.FirstOrDefault(x => x.Value == building);
            return item.Key;
        }
        internal UpgradeOperation StartUpgrade(Guid key, Building building, int upgrade)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            var tile = GetTile(key, building);
            var status = m_upgradeService.StartUpgrade(tile, building, upgrade);
            if (status.Status != UpgradeStatus.Failed)
                NotifyBuildingChange(status.From, status.Tile, ChangeType.Upgrading, status.To);
            return status;
        }
        internal bool AbortUpgrade(Guid key, Building building)
        {
            if(!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            if (m_upgradeService.TryAbortUpgrade(building, out var operation))
            {
                NotifyBuildingChange(operation.From, operation.Tile, ChangeType.UpgradeStopped);
                return true;
            }
            return false;
        }
        internal bool PauseUpgrade(Guid key, Building building)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            var result =  m_upgradeService.TryPauseUpgrade(building, out var operation);
            if(result)
                NotifyBuildingChange(operation.From, operation.Tile, ChangeType.UpgradeStopped, operation.To);
            return result;
        }
        internal bool ResumeUpgrade(Guid key, Building building)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            var result = m_upgradeService.ResumeUpgrade(building);
            if (result)
            {
                var operation = m_upgradeService.GetUpgradeStatus(building);
                NotifyBuildingChange(operation.From, operation.Tile, ChangeType.Upgrading, operation.To);
            }

            return result;
        }
        private void NotifyBuildingChange(Building building, Tile tile, ChangeType type, Building newBuilding = null)
        {
            if (building == null)
                throw new ArgumentNullException(nameof(building));
            if (tile == null)
                throw new ArgumentNullException(nameof(tile));
            BuildingEventPublisher.Create(m_eventPublisher, tile, building, type, newBuilding);
        }

        private void UpgradeComplete(UpgradeOperation info)
        {
            SetBuilding(m_key, info.Tile, info.To);
        }
    }
}

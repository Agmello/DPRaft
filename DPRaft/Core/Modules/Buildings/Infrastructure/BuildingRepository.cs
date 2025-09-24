using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Tiles.Domain;

namespace Core.Modules.Buildings.Infrastructure
{
    internal class BuildingRepository : IBuildingRepository
    {
        private BuildingBank m_buildingBank;
        private IEventPublisher m_eventPublisher;
        private Guid m_key;
        public BuildingRepository(IEventPublisher eventPublisher,ITileBuildingFactory factory)
        {
            m_eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
            m_key = Guid.NewGuid();
            m_buildingBank = new BuildingBank(m_key, m_eventPublisher, factory);
        }

        public IEnumerable<Building> GetAllBuildings()
        {
            return m_buildingBank.GetAllBuildings(m_key);
        }
        public IEnumerable<(Tile tile, Building building)> GetAll()
        {
            return m_buildingBank.GetAll(m_key);
        }

        public void AddBuilding(Tile tile, Building building)
        {
            m_buildingBank.AddBuilding(m_key, tile, building);
        }

        public Building DestroyBuilding(Tile tile)
        {
            return m_buildingBank.RemoveBuilding(m_key, tile);
        }

        public void UpgradeBuilding(Tile tile, Building building)
        {
            m_buildingBank.UpgradeBuilding(m_key, tile, building);
        }
        public void RemoveBuildingSpot(Guid key, Tile tile)
        {
            m_buildingBank.RemoveBuildingSpot(key, tile);
        }
    }
}

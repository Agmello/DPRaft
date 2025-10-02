using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Application.Contracts;
using Core.Modules.Buildings.Application.Services;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Tiles.Domain;

namespace Core.Modules.Buildings.Infrastructure
{
    internal class BuildingRepository : IBuildingRepository
    {
        private BuildingBank m_buildingBank;
        private Guid m_key;
        public BuildingRepository(
            IEventPublisher eventPublisher,
            ITileBuildingFactory factory,
            IUpgradeManager manager,
            IUpgradeService service)
        {
            m_key = Guid.NewGuid();
            m_buildingBank = new BuildingBank(m_key, eventPublisher, manager, service, factory);
        }

        public IEnumerable<Building> GetAllBuildings()
        {
            return m_buildingBank.GetAllBuildings(m_key);
        }
        public Building GetBuilding(Tile tile)
        {
            return m_buildingBank.GetBuilding(m_key, tile);
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

        public void SetBuilding(Tile tile, Building building)
        {
            m_buildingBank.SetBuilding(m_key, tile, building);
        }
        public void RemoveBuildingSpot(Tile tile)
        {
            m_buildingBank.RemoveBuildingSpot(m_key, tile);
        }

        public Tile GetTileFromBuilding(Building building)
        {
            return m_buildingBank.GetTile(m_key, building);
        }

        public UpgradeOperation StartUpgrade(Building building, int upgrade)
        {
            return m_buildingBank.StartUpgrade(m_key, building, upgrade);
        }

        public bool AbortUpgrade(Building building)
        {
            return m_buildingBank.AbortUpgrade(m_key, building);
        }

        public bool PauseUpgrade(Building building)
        {
            return m_buildingBank.PauseUpgrade(m_key, building);
        }

        public bool ResumeUpgrade(Building building)
        {
            return m_buildingBank.ResumeUpgrade(m_key, building);
        }
    }
}

using Core.Modules.Buildings.Application.Services;
using Core.Modules.Buildings.Domain.Events;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Domain.Contracts
{
    public interface IBuildingRepository
    {
        public IEnumerable<Building> GetAllBuildings();
        public Building GetBuilding(Tile tile);
        public IEnumerable<(Tile tile, Building building)> GetAll();
        public void AddBuilding(Tile tile, Building building);
        public Building DestroyBuilding(Tile tile);
        public void SetBuilding(Tile tile, Building building);
        public void AddBuildingSpot(Tile tile);
        public void RemoveBuildingSpot(Tile tile);
        public Tile GetTileFromBuilding(Building building);
        internal UpgradeOperation StartUpgrade(Building building, int upgrade);
        internal bool AbortUpgrade(Building building);
        internal bool PauseUpgrade(Building building);
        internal bool ResumeUpgrade(Building building);
    }
}

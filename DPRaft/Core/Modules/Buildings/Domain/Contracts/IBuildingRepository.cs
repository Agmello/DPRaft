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
        public IEnumerable<(Tile tile, Building building)> GetAll();
        public void AddBuilding(Tile tile, Building building);
        public Building DestroyBuilding(Tile tile);
        public void UpgradeBuilding(Tile tile, Building building);
        public void RemoveBuildingSpot(Guid key, Tile tile);
    }
}

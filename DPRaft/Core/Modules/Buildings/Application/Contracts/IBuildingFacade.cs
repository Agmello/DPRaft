using Core.Modules.Buildings.Domain;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Application.Contracts
{
    public interface IBuilding
    {
        // Methods to manage buildings on tiles
        public Building GetBuilding(Tile tile);
        public IEnumerable<(Tile, Building)> GetAllBuildings();
        public Building PlaceBuilding(Tile tile, string building);
        public Building RemoveBuilding(Tile tile);
        // Upgrade related methods
        public UpgradeInfo GetUpgradeInfo(Tile tile);
        public UpgradeInfo UpgradeBuilding(Tile tile, string building);
        public void AbortUpgrade(Tile tile);
    }
}

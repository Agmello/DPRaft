using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Application
{
    internal class BuildingFacade
    {
        public Tile Tile { get; }
        public Building Building { get; set; }
        public BuildingFacade(Tile tile, Building building)
        {
            Tile = tile;
            Building = building;
        }
        public void Upgrade(string building)         {
            var upgradeInfo = Building.AvailableUpgrades.FirstOrDefault(u => u.Name == building);
            if (upgradeInfo == null)
                throw new InvalidOperationException($"Cannot upgrade building {Building.Name} on tile ({Tile.X}, {Tile.Y}) to {building}");
            
        }
        public void Upgrade(int choice)
        {
            var upgrade = Building.AvailableUpgrades.ElementAt(choice);

        }
    }
}

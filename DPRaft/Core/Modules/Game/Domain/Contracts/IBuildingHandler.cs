using Core.Modules.Buildings.Application.Contracts;
using Core.Modules.Buildings.Domain;
using Core.Modules.Game.Domain.Facades;
using Core.Modules.Resources.Application.Dtos;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBuilding = Core.Modules.Buildings.Application.Contracts.IBuilding;

namespace Core.Modules.Game.Domain.Contracts
{
    public interface IBuildingHandler
    {
        string[] PossibleBuildings(ITile tile);
        (int, ResourceDto)[] GetSettings(IBuilding building);
        bool ConstructBuilding(string buildingName, int x, int y);
        bool ConstructBuilding(string buildingName, ITile tile);
        bool RazeBuilding(Tile tile);
        bool RazeBuilding(IBuilding tile);
        
    }
}

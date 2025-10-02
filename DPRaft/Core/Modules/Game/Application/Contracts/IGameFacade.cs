using Core.Modules.Buildings.Domain;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Game.Application.Contracts
{
    public interface IGame
    {
        public void ExeceuteTick();
        public Tile GetTile(int x, int y);
        public IEnumerable<Tile> GetAllTiles();



        
    }
}

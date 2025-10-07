using Core.Modules.Tiles.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Tiles.Domain.Contracts
{
    internal interface ITileMap
    {
        public Tile? GetTile(int x, int y);
        public IEnumerable<Tile> GetAllTiles();
        public void AddTile(Tile tile);
        public void RemoveTile(Tile tile);
    }
}

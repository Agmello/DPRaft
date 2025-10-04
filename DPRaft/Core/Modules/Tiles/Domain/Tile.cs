using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Tiles.Domain
{
    public class Tile
    {
        public int X { get; internal set; }
        public int Y { get; internal set; }
        public Tile() { }
        internal Tile(int x, int y)
        {
            X = x;
            Y = y;
        }

    }
}

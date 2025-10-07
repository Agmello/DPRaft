using Core.Modules.Buildings.Domain.Events;
using Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Tiles.Domain.Events
{
    internal class TileEvent : IEvent
    {
        public Tile Tile { get; }
        public ChangeType ChangeType { get; }
        
        public TileEvent(Tile tile, ChangeType changeType)
        {
            Tile = tile ?? throw new ArgumentNullException(nameof(tile));
            ChangeType = changeType;
        }
    }
}

using Core.Modules.Buildings.Domain;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Application.Services
{
    internal class UpgradeInformation(
        Tile tile,
        Building to,
        Building from,
        int ticksLeft
        )
    {
        public UpgradeStatus Status { get; set; } = UpgradeStatus.Upgrading;
        public Tile Tile { get; } = tile;
        public Building To { get; } = to;
        public Building From { get; } = from;
        public int TicksLeft { get; set; } = ticksLeft;
    }
}

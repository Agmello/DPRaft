using Core.Modules.Buildings.Domain;
using Core.Modules.Resources.Application.Dtos;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Application.Services
{
    public class UpgradeOperation
    {
        internal UpgradeOperation(
            Tile tile,
            Building to,
            Building from,
            int ticksLeft,
            IEnumerable<ResourceDto> cost
            )
        {
            Tile = tile ?? throw new ArgumentNullException(nameof(tile));
            To = to; //?? throw new ArgumentNullException(nameof(to));
            From = from ?? throw new ArgumentNullException(nameof(from));
            TicksLeft = ticksLeft;
            Cost = cost ?? Enumerable.Empty<ResourceDto>();
        }

        internal static UpgradeOperation CreateFailed(Tile tile, Building from)
        {
            var ui = new UpgradeOperation(tile, null, from, 0, null);
            ui.Status = UpgradeStatus.Failed;
            return ui;
        }

        public UpgradeStatus Status { get; internal set; } = UpgradeStatus.Upgrading;
        public Tile Tile { get; }
        public Building To { get; }
        public Building From { get; }
        public int TicksLeft { get; internal set; }
        public IEnumerable<ResourceDto> Cost { get; }
    }
}

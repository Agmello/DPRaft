using Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Domain.Events
{
    public class YieldBuildingEvent : BuildingChangedEvent
    {
        public string Building { get; }
        public YieldBuildingEvent(string b) :
            base(new Tiles.Domain.Tile(), null, ChangeType.Added)
        {
            Building = b;
        }
    }
}

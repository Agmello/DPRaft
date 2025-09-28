using Core.Modules.Tiles.Domain;
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
        public YieldBuildingEvent(string b) :
            base(new Tiles.Domain.Tile(), null, ChangeType.Added)
        {
        }
        public YieldBuildingEvent(Tile t, Building b, ChangeType ct) :
            base(t, b, ct)
        {
        }
    }
}

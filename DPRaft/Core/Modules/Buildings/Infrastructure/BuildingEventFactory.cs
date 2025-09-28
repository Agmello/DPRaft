using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Events;
using Core.Modules.Tiles.Domain;
using Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Infrastructure
{
    internal class BuildingEventPublisher
    {
        public static void Create(IEventPublisher publisher,Tile tile, Building building, ChangeType eventType)
        {
            var (type,@event) = building switch
            {
                ProductionBuilding pb => (typeof(YieldBuildingEvent), new YieldBuildingEvent(tile, pb, eventType)),
                _ => (typeof(BuildingChangedEvent), new BuildingChangedEvent(tile, building, eventType))
            };
            publisher.Publish(type, @event);
        }
    }
}

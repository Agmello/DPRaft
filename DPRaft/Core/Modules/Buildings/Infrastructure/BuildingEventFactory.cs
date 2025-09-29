using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Buildings;
using Core.Modules.Buildings.Domain.Events;
using Core.Modules.Tiles.Domain;

namespace Core.Modules.Buildings.Infrastructure
{
    internal class BuildingEventPublisher
    {
        public static void Create(IEventPublisher publisher,Tile tile, Building building, ChangeType eventType, Building? newBuilding = null)
        {
            var (type,@event) = building switch
            {
                ResourceBuilding pb => (typeof(ResourceBuildingEvent), new ResourceBuildingEvent(tile, pb, eventType, newBuilding)),
                _ => (typeof(BuildingChangedEvent), new BuildingChangedEvent(tile, building, eventType, newBuilding))
            };
            publisher.Publish(type, @event);
        }
    }
}

using Core.Modules.Tiles.Domain;

namespace Core.Modules.Buildings.Domain.Events
{
    public class ResourceBuildingEvent : BuildingChangedEvent
    {
        public ResourceBuildingEvent(string b) :
            base(new Tile(), null, ChangeType.Added, null)
        {
        }
        public ResourceBuildingEvent(Tile t, Building b, ChangeType ct, Building? newBuilding = null) :
            base(t, b, ct, newBuilding)
        {
        }
    }
}

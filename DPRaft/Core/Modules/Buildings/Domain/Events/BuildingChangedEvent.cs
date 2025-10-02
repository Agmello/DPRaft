using Core.Modules.Tiles.Domain;
using Core.SharedKernel;

namespace Core.Modules.Buildings.Domain.Events
{
    public enum ChangeType
    {
        Added,
        Removed,
        Upgrading,
        Upgraded,
        UpgradeStopped
    }
    public class BuildingChangedEvent : IEvent
    {
        public Tile Tile { get; } 
        public virtual Building Building { get; }
        public virtual Building? NewBuilding { get; } 
        public ChangeType Change { get; }
        public BuildingChangedEvent(Tile tile,Building building, ChangeType change, Building? newBuilding = null)
        {
            Tile = tile;
            Building = building;
            Change = change;
            NewBuilding = newBuilding;
        }
    }
}

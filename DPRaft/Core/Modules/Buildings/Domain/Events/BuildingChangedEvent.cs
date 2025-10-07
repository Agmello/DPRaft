using Core.Modules.Resources.Domain.Events;
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
    public class BuildingChangedEvent : Event
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
        public override string LogMessage()
        {
            var ret =  $"{nameof(BuildingChangedEvent)}: {Building.Name} @ {Tile?.X},{Tile?.Y}) <{Change}>";
            ret += Change switch
            {
                ChangeType.Upgrading or
                ChangeType.Upgraded or
                ChangeType.UpgradeStopped => $" to {NewBuilding?.Name}",
                _ => ""
            };
            return ret ;
        }
    }
}

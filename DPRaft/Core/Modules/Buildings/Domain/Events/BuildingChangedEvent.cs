using Core.SharedKernel;

namespace Core.Modules.Buildings.Domain.Events
{
    public enum ChangeType
    {
        Added,
        Removed,
        Updated
    }
    public class BuildingChangedEvent : IEvent
    {
        public Building Building { get; }
        public ChangeType Change { get; }
        public BuildingChangedEvent(Building building, ChangeType change)
        {
            Building = building;
            Change = change;
        }
    }
}

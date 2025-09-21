using Core.Abstractions;
using Core.Domains.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
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

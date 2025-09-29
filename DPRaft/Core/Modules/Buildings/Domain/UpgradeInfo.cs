using Core.Modules.Resources.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Domain
{
    public class UpgradeInfo
    {
        public string Name { get; }
        public List<ResourceDto> Costs { get; } = new();
        public int TicksToComplete { get; }

        public UpgradeInfo(string name, IEnumerable<ResourceDto> costs, int ticksToComplete)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            if (costs == null) throw new ArgumentNullException(nameof(costs));
            Costs.AddRange(costs);
            if (ticksToComplete <= 0) throw new ArgumentOutOfRangeException(nameof(ticksToComplete), "Ticks to complete must be greater than zero.");
            TicksToComplete = ticksToComplete;
        }
    }
}

using Core.Domains.Buildings.Actions;
using Core.Domains.Buildings.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains.Buildings
{
    public abstract class Building
    {
        abstract public string Name { get; }
        protected readonly List<Output> m_outputs = new();
        protected abstract Upgrade[] Upgrades { get; }
        public IReadOnlyList<Output> Outputs => m_outputs;
        public IReadOnlyList<Upgrade> AvailableUpgrades => Upgrades;
    }
}

using Core.Domains.Buildings.Actions;
using Core.Domains.Buildings.Resources;
using Core.Modules.Resources.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Domain
{
    public abstract class Building
    {
        abstract public string Name { get; }
        protected readonly List<ResourceDto> m_outputs = new();
        protected abstract UpgradeInfo[] Upgrades { get; }
        public IReadOnlyList<ResourceDto> Outputs => m_outputs;
        public IReadOnlyList<UpgradeInfo> AvailableUpgrades => Upgrades;
    }
}

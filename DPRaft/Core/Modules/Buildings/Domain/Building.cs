using Core.Modules.Resources.Application.Dtos;

namespace Core.Modules.Buildings.Domain
{
    public abstract class Building
    {
        abstract public string Name { get; }
        //protected abstract ResourceDto[] m_outputs { get; }
        protected abstract UpgradeInfo[] Upgrades { get; }
        
        //public IReadOnlyList<ResourceDto> Outputs => m_outputs;
        //public IReadOnlyList<UpgradeInfo> AvailableUpgrades => Upgrades;
        public IEnumerable<UpgradeInfo> AvailableUpgrades => Upgrades;

    }
}

using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class SolarPurifier : ResourceBuilding, IYield
    {
        public override string Name => "Solar Purifier";

        protected override ResourceYield[] m_productions => [
                new ResourceYield("Water", 3),
                new ResourceYield("Glass", 1, true)
            ];

        protected override UpgradeInfo[] Upgrades => [];
    }
}

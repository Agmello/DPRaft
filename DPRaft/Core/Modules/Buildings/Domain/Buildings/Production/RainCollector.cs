using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class RainCollector : ResourceBuilding, IProducer
    {
        public override string Name => "Rain Collector";

        protected override ResourceYield[] m_productions => [
                new ResourceYield("Water", 1)
            ];

        protected override UpgradeInfo[] Upgrades => [
                new UpgradeInfo("Gutters", 
                    [
                        new ResourceDto("Wood", 5)
                    ], 5),
                new UpgradeInfo("Solar Purifier", 
                    [
                        new ResourceDto("Wood", 5),
                        new ResourceDto("Glass", 2)
                    ], 5),
            ];
    }
}

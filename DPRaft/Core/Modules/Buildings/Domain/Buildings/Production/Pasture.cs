using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class Pasture : ResourceBuilding, IProducer
    {
        public override string Name => "Pasture";

        protected override ResourceYield[] m_productions => [
                new ResourceYield("Leather", 1)
            ];

        protected override UpgradeInfo[] Upgrades => [
                new UpgradeInfo("Pasturage", [new ResourceDto("Food", 5)], 5),
                new UpgradeInfo("Ranch", [new ResourceDto("Wood",3), new ResourceDto("Food", 2)], 5),
            ];
    }
}

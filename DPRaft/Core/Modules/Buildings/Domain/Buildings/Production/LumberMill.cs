using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class LumberMill : ResourceBuilding, IProducer
    {
        public override string Name => "Lumber Mill";

        protected override ResourceYield[] m_productions => [
                new ResourceYield("Wood", 1)
            ];

        protected override UpgradeInfo[] Upgrades => [
                new UpgradeInfo("Sawmill", [new ResourceDto("Wood", 5)], 5),
                new UpgradeInfo("Orchard", [new ResourceDto("Wood", 4), new ResourceDto("Food",1)], 5)
            ];
    }
}

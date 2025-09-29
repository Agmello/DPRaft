using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class Diver : ResourceBuilding, IProducer
    {
        public override string Name => "Diver";

        protected override ResourceYield[] m_productions => [
                new ResourceYield("Stone", 1,setting: 1),
                new ResourceYield("Sand", 1,setting: 2),
            ];

        protected override UpgradeInfo[] Upgrades => [
                new UpgradeInfo("Wet bell", 
                    [
                        new ResourceDto("Wood", 2),
                        new ResourceDto("Stone", 3)
                    ], 5),
                new UpgradeInfo("Scuba",
                    [
                        new ResourceDto("Wood", 1),
                        new ResourceDto("Leather", 3),
                        new ResourceDto("Glass", 2)
                    ], 5),
            ];
    }
}

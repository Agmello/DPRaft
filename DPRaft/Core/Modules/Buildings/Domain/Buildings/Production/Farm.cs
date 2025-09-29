using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class Farm : ResourceBuilding, IProducer
    {
        private static string m_name = "Farm";
        public override string Name => m_name;
        protected double m_yieldMultiplier { get; set; } = 1.0;

        protected override UpgradeInfo[] Upgrades => [
                new UpgradeInfo("Orchard",[new ResourceDto("Wood", 5.0)],4),
                new UpgradeInfo("Alotment",
                    [
                        new ResourceDto("Wood", 4.0),
                        new ResourceDto("Stone", 1)
                    ],5),
                new UpgradeInfo("Ranch",
                    [
                        new ResourceDto("Wood", 4.0),
                        new ResourceDto("Stone", 1),
                        new ResourceDto("Food", 2)
                    ],5),
            ];

        protected override ResourceYield[] m_productions => [
                new ResourceYield("Food", 1.0)
            ];
    }
}

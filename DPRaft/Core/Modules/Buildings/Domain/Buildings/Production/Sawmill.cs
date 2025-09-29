using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class Sawmill : ResourceBuilding, IProducer
    {
        public override string Name => "Sawmill";

        protected override ResourceYield[] m_productions =>
            [
                new ResourceYield("Wood", 2)
            ];

        protected override UpgradeInfo[] Upgrades => [];
    }
}

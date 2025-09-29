using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class BigFarm : ResourceBuilding, IProducer
    {
        public override string Name => "Big Farm";

        protected override ResourceYield[] m_productions => [
                new ResourceYield("Food", 2),
            ];
    }
}

using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class Orchard : ResourceBuilding, IProducer
    {
        public override string Name => "Orchard";

        protected override ResourceYield[] m_productions => [
            new ResourceYield("Food", 1),
            new ResourceYield("Wood", 1)
            ];
    }
}

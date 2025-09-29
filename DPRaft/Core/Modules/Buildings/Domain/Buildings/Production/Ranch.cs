using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class Ranch : ResourceBuilding, IProducer
    {
        public override string Name => "Ranch";

        protected override ResourceYield[] m_productions => [
                new ResourceYield("Food", 1),
                new ResourceYield("Leather", 1),
            ];

    }
}

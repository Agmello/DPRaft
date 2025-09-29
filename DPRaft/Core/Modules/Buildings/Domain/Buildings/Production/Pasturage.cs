using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class Pasturage : ResourceBuilding, IProducer
    {
        public override string Name => "Pasturage";

        protected override ResourceYield[] m_productions => [
                new ResourceYield("Leather", 2)
            ];
    }
}

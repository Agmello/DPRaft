using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class Gutters : ResourceBuilding, IProducer
    {
        public override string Name => "Gutters";

        protected override ResourceYield[] m_productions => [
                new ResourceYield("Water", 2)
            ];
    }
}

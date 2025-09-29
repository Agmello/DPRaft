using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class WetBell : ResourceBuilding, IProducer
    {
        public override string Name => "Wet Bell";
        public int Setting { get; set; } = 1;

        protected override ResourceYield[] m_productions => [
                new ResourceYield("Sand", 2, setting: 1),
                new ResourceYield("Stone", 2, setting: 2),
            ];
    }
}

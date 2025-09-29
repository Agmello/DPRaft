using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Domain;

namespace Core.Modules.Buildings.Domain.Buildings.Production
{
    internal class Scuba : ResourceBuilding, IYield
    {
        public override string Name => "Scuba";

        protected override ResourceYield[] m_productions => [
                new ResourceYield("Sand", 3, setting: 1),
                new ResourceYield("Stone", 3, setting: 2),
                new ResourceYield("Glass", 1, true)
            ];
    }
}

using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;

namespace Core.Modules.Buildings.Domain
{
    internal class Farm : Building, IYield
    {
        private static string m_name = "Farm";
        public override string Name => m_name;

        protected override UpgradeInfo[] Upgrades => [
            ];

        private double YieldModifier() => 1.0;
        public IEnumerable<ResourceDto> Get()
        {
            return [new ResourceDto("Food", 1.0 * YieldModifier())];
        }
    }
}

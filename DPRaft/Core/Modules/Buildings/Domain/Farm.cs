using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;

namespace Core.Modules.Buildings.Domain
{
    internal class Farm : ProductionBuilding
    {
        private static string m_name = "Farm";
        public override string Name => m_name;
        protected double m_yieldMultiplier { get; set; } = 1.0;

        protected override UpgradeInfo[] Upgrades => [
            ];

        protected override ResourceDto[] m_productions => [
                new ResourceDto("Food", 1.0)
            ];

        public override IEnumerable<ResourceDto> CreateProduction()
        {
            throw new NotImplementedException();
        }
        public override IEnumerable<ResourceDto> UseResources()
        {
            throw new NotImplementedException();
        }
        
    }
}

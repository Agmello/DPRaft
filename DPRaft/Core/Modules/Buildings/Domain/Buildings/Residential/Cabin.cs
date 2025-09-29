using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;

namespace Core.Modules.Buildings.Domain.Buildings.Residential
{
    internal class Cabin : Building, IResidentialBuilding
    {
        public override string Name => nameof(Cabin);
        public int Capacity => 2;
        protected override UpgradeInfo[] Upgrades => [
            new UpgradeInfo("Lodge", [
                new ResourceDto("Wood", 5),
                new ResourceDto("Stone", 5)
            ], 5)
        ];
    }
}

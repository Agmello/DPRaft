using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;

namespace Core.Modules.Buildings.Domain.Buildings.Residential
{
    internal class Hut : Building, IResidentialBuilding
    {
        public override string Name => nameof(Hut);
        public int Capacity => 1;
        protected override UpgradeInfo[] Upgrades =>
            [
                new UpgradeInfo("Cabin",
                    [
                    new ResourceDto("Wood", 5),
                    ], 5),
                new UpgradeInfo("Alotment",
                    [
                    new ResourceDto("Wood", 3),
                    new ResourceDto("Food", 2),
                    ], 5),
            ];

    }
}

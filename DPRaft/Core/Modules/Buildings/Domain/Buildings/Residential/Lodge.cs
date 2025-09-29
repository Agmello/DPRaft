using Core.Modules.Buildings.Domain.Contracts;

namespace Core.Modules.Buildings.Domain.Buildings.Residential
{
    internal class Lodge : Building, IResidentialBuilding
    {
        public override string Name => nameof(Lodge);
        public int Capacity => 2;
    }
}

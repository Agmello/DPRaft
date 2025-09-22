using Information.Patterns;

namespace Core.Modules.Buildings.Domain.Contracts
{
    public interface ITileBuildingFactory : IFactory
    {
        public Building Create(string key);
    }
}

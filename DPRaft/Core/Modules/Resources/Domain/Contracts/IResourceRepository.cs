using Core.Modules.Buildings.Domain;
using Information.Patterns;

namespace Core.Modules.Resources.Domain.Contracts
{
    public interface IResourceRepository : ISingleton
    {
        public void AddBuilding(Building building);
        public void AddResources(string resource, int value);
    }
}

using Core.Modules.Buildings.Domain;
using Information.Patterns;

namespace Core.Modules.Resources.Domain.Contracts
{
    public interface IResourceRepository : ISingleton
    {
        double AddResources(string resource, double value);
        double UseResources(string resource, double value);
        IEnumerable<(string Resource, double Amount)> GetAll();
        double Get(string resource);
    }
}

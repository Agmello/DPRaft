using Core.Modules.Buildings.Domain;
using Core.Modules.Resources.Application.Dtos;
using Information.Patterns;

namespace Core.Modules.Resources.Domain.Contracts
{
    public interface IResourceRepository : ISingleton
    {
        double AddResource(string resource, double value);
        void AddResources(IEnumerable<ResourceDto> resources);
        double UseResource(string resource, double value);
        bool UseResources(IEnumerable<ResourceDto> resources);
        IEnumerable<(string Resource, double Amount)> GetAll();
        double Get(string resource);
    }
}

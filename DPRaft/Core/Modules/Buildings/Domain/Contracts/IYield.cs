using Core.Modules.Resources.Application.Dtos;

namespace Core.Modules.Buildings.Domain.Contracts
{
    public interface IYield
    {
        public IEnumerable<ResourceDto> Get();
    }
}

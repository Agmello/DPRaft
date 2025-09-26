using Core.Modules.Resources.Application.Dtos;

namespace Core.Modules.Buildings.Domain.Contracts
{
    public interface IProducer
    {
        public IEnumerable<ResourceDto> CreateProduction();
    }
    public interface IConsumer
    {
        public IEnumerable<ResourceDto> CreateConsumption();
    }
    public interface IYield : IProducer, IConsumer
    {
        public IEnumerable<ResourceDto> CreateYield();
    }
}

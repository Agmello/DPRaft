using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Contracts;
using Core.Modules.Resources.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Resources.Application.Services
{
    internal class YieldComposite : IYieldComposite
    {
        private List<IConsumer> Consumers { get; } = new();
        private List<IProducer> Producers { get; } = new();
        public IEnumerable<ResourceDto> CreateConsumption()
        {
            return Consumers
                .SelectMany(p => 
                    p.CreateConsumption())
                .GroupBy(c => c.Key)
                .Select(g => new ResourceDto(g.Key, g.Sum(s => s.Amount)));
        }

        public IEnumerable<ResourceDto> CreateProduction()
        {
            return Producers
                .SelectMany(p =>
                    p.CreateProduction())
                .GroupBy(c => c.Key)
                .Select(g => new ResourceDto(g.Key, g.Sum(s => s.Amount)));
        }

        public IEnumerable<ResourceDto> CreateYield()
        {
            var production = CreateProduction();
            var consumption = CreateConsumption();
            return production
                .Concat(consumption.Select(c => new ResourceDto(c.Key, -c.Amount)))
                .GroupBy(r => r.Key)
                .Select(g => new ResourceDto(g.Key, g.Sum(s => s.Amount)));
        }
        public bool TryRegisterConsumer(IConsumer consumer)
        {
            if (Consumers.Contains(consumer))
                return false;
            Consumers.Add(consumer);
            return true;
        }

        public bool TryRegisterProducer(IProducer producer)
        {
            if (Producers.Contains(producer))
                return false;
            Producers.Add(producer);
            return true;
        }

        public bool TryRegisterYield(IYield yield)
        {
            if(Producers.Contains(yield) && Consumers.Contains(yield))
                return false;

            if(!Producers.Contains(yield))
                Producers.Add(yield);
            if(!Consumers.Contains(yield))
                Consumers.Add(yield);
            return true;
        }

        public bool TryUnregisterConsumer(IConsumer consumer)
        {
            return Consumers.Remove(consumer);
        }

        public bool TryUnregisterProducer(IProducer producer)
        {
            return Producers.Remove(producer);
        }

        public bool TryUnregisterYield(IYield yield)
        {
            return TryUnregisterConsumer(yield) || TryUnregisterProducer(yield);
        }
    }
}

using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Resources.Application.Contracts
{
    internal interface IResourceMediator
    {   
        bool CanConsumeResource(string resourceName, double amount);
        bool CanConsumeResources(IEnumerable<(string ResourceName, double Amount)> resources);
        bool TryConsumeResource(string resourceName, double amount);
        bool TryConsumeResources(IEnumerable<(string ResourceName, double Amount)> resources);
        void ProduceResource(string resourceName, double amount);
        void ProduceResources(IEnumerable<(string ResourceName, double Amount)> resources);
        double GetResourceAmount(string resourceName);
        IEnumerable<(string ResourceName, double Amount)> GetAllResources();
        bool TryRegisterResourceYield(IYield building);
        bool TryUnregisterResourceYield(IYield building);
        bool TryRegisterResourceProducer(IProducer producer);
        bool TryUnregisterResourceProducer(IProducer producer);
        bool TryRegisterResourceConsumer(IConsumer consumer);
        bool TryUnregisterResourceConsumer(IConsumer consumer);


    }
}

using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Contracts;
using Core.Modules.Resources.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Resources.Application.Services
{
    internal class ResourceMediator : IResourceMediator
    {
        IResourceRepository m_resourceRepository;
        IYieldComposite m_yieldComposite;
        public ResourceMediator(IResourceRepository resourceRepository, IYieldComposite yieldComposite)
        {
            m_resourceRepository = resourceRepository;
            m_yieldComposite = yieldComposite;
        }

        public bool CanConsumeResource(string resourceName, double amount)
        {
            return amount <= m_resourceRepository.Get(resourceName);
        }

        public bool CanConsumeResources(IEnumerable<(string ResourceName, double Amount)> resources)
        {
            return resources.All(r => CanConsumeResource(r.ResourceName, r.Amount));
        }

        public IEnumerable<(string ResourceName, double Amount)> GetAllResources()
        {
            return m_resourceRepository.GetAll();
        }

        public double GetResourceAmount(string resourceName)
        {
            return m_resourceRepository.Get(resourceName);
        }

        public void ProduceResource(string resourceName, double amount)
        {
            m_resourceRepository.AddResource(resourceName, amount);
        }

        public void ProduceResources(IEnumerable<(string ResourceName, double Amount)> resources)
        {
            foreach(var resource in resources)
            {
                m_resourceRepository.AddResource(resource.ResourceName, resource.Amount);
            }
        }
        public bool TryConsumeResource(string resourceName, double amount)
        {
            if(!CanConsumeResource(resourceName, amount))
                return false;
            m_resourceRepository.UseResource(resourceName, amount);
            return true;
        }

        public bool TryConsumeResources(IEnumerable<(string ResourceName, double Amount)> resources)
        {
            if(!CanConsumeResources(resources))
                return false;
            foreach(var resource in resources)
            {
                m_resourceRepository.UseResource(resource.ResourceName, resource.Amount);
            }
            return true;
        }

        public bool TryRegisterResourceConsumer(IConsumer consumer) => m_yieldComposite.TryRegisterConsumer(consumer);

        public bool TryRegisterResourceProducer(IProducer producer) => m_yieldComposite.TryRegisterProducer(producer);

        public bool TryRegisterResourceYield(IYield building) => m_yieldComposite.TryRegisterYield(building);

        public bool TryUnregisterResourceConsumer(IConsumer consumer) => m_yieldComposite.TryUnregisterConsumer(consumer);

        public bool TryUnregisterResourceProducer(IProducer producer) => m_yieldComposite.TryUnregisterProducer(producer);

        public bool TryUnregisterResourceYield(IYield building) => m_yieldComposite.TryUnregisterYield(building);

    }
}

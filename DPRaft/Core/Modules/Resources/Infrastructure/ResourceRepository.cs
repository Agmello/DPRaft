using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Events;
using Core.Modules.Resources.Application.Dtos;
using Core.Modules.Resources.Domain;
using Core.Modules.Resources.Domain.Contracts;
using Information.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Resources.Infrastructure
{
    internal class ResourceRepository : IResourceRepository
    {
        private Guid m_key;
        private ResourceBank m_resourceBank;
        public ResourceRepository(IEventPublisher eventPublisher)
        {
            m_key = Guid.NewGuid();
            m_resourceBank = new ResourceBank(m_key, eventPublisher);
        }
        public double Get(string resource)
        {
            return m_resourceBank.Get(m_key, resource);
        }

        public IEnumerable<(string Resource, double Amount)> GetAll()
        {
            throw new NotImplementedException();
        }

        public double AddResource(string resource, double value)
        {
            return m_resourceBank.AddResources(m_key, resource, value); 
        }

        public void AddResources(IEnumerable<ResourceDto> resources)
        {
            foreach(var resource in resources)
            {
                m_resourceBank.AddResources(m_key, resource.Key, resource.Amount);
            }
        }

        public double UseResource(string resource, double value)
        {
            return m_resourceBank.RemoveResources(m_key, resource, value);
        }

        public bool UseResources(IEnumerable<ResourceDto> resources)
        {
            if(resources.Any(x => x.Amount > m_resourceBank.Get(m_key, x.Key))) return false;

            foreach(var resource in resources)
            {
                m_resourceBank.RemoveResources(m_key, resource.Key, resource.Amount);
            }
            return true;
        }
    }
}

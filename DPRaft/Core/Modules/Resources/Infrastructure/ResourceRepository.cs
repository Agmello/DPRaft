using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Events;
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

        public double UseResources(string resource, double value)
        {
            throw new NotImplementedException();
        }

        public double AddResources(string resource, double value)
        {
            return m_resourceBank.AddResources(m_key, resource, value); 
        }
    }
}

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

        public void AddBuilding(Building building)
        {
            throw new NotImplementedException();
        }

        public void AddResources(string resource, int value)
        {
            throw new NotImplementedException();
        }
    }
}

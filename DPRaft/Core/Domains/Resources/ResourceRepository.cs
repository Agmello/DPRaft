using Core.Abstractions;
using Core.Domains.Buildings;
using Core.Events;
using Core.Observers;
using Information.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains.Resources
{
    internal class ResourceRepository : IResourceRepository
    {
        private Guid m_key;
        private ResourceBank m_bank;
        private BuildingObserver buildingObserver;

        public void AddBuilding(Building building)
        {
            var buildings = m_bank.Buildings(m_key);
            if(buildings.Contains(building))
                return;
            buildings.Add(building);

            buildingObserver.Notify(new BuildingChangedEvent(building, ChangeType.Added));
        }

        public void AddResources(string resource, int value)
        {
            var resourceBank = m_bank.Bank(m_key);
            if (resourceBank.ContainsKey(resource))
                resourceBank[resource] += value;
            else
                resourceBank.Add(resource, value);
        }
    }
}

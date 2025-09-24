using Core.BuildingBlocks.Messaging;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Game.Domain.Banks;
using Core.Modules.Game.Domain.Contracts;
using Core.Modules.Resources.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Game.Domain
{
    internal class GameSession : IResourceHandler, IHumanHandler, IBuildingHandler
    {
        private IBuildingRepository m_buildingRepository;
        private IResourceRepository m_resourceRepository;

        public GameSession(
            IBuildingRepository buildingRepository,
            IResourceRepository resourceRepository
            )
        {
            m_buildingRepository = buildingRepository ?? throw new ArgumentNullException(nameof(buildingRepository));
            m_resourceRepository = resourceRepository ?? throw new ArgumentNullException(nameof(resourceRepository));
        }

        public IEnumerable<(string resource, double amount)> Resources()
        {
            return m_resourceRepository.GetAll();
        }
        public double AddResources(string resource, double value)
        {
            return m_resourceRepository.AddResources(resource, value);
        }

        public double RemoveResources(string resource, double value)
        {
            return m_resourceRepository.UseResources(resource, value);
        }
        internal IEnumerable<Building> Buildings()
        {
            return m_buildingRepository.GetAllBuildings();
        }


    }
}

using Core.Modules.Buildings.Application.Contracts;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;
using Core.Modules.Resources.Domain.Contracts;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Application.Services
{
    internal class UpgradeService : IUpgradeService
    {
        private IUpgradeManager m_upgradeManager;
        private ITileBuildingFactory m_factory;
        private IBuildingRepository m_repository;
        private IResourceRepository m_resourceRepository;

        private bool CanUpgrade(UpgradeInfo info, out IEnumerable<ResourceDto> errs)
        {
            errs = info.Costs.Where(x => m_resourceRepository.Get(x.Key) > x.Amount).ToList();
            return !errs.Any();
        }

        public bool AbortUpgrade(Building building)
        {
            return TryAbortUpgrade(building, out var _);
        }
        public bool TryAbortUpgrade(Building building, out UpgradeOperation operation)
        {
            if (m_upgradeManager.TryAbortUpgrade(building, out operation))
            {
                m_resourceRepository.AddResources(operation.Cost);
                return true;
            }
            return false;
        }
        public UpgradeOperation StartUpgrade(Tile tile, Building building, int upgradeNum)
        {
            var upgrade = building.AvailableUpgrades.ElementAtOrDefault(upgradeNum);

            if (CanUpgrade(upgrade, out var err))
            {
                // TODO: Add failed event
                return UpgradeOperation.CreateFailed(tile, building);
            }
            var upgradeBuilding = m_factory.Create(upgrade.Name);
            var operation = new UpgradeOperation(tile,upgradeBuilding, building, upgrade.TicksToComplete, upgrade.Costs);
            m_upgradeManager.StartUpgrade(operation);
            m_resourceRepository.UseResources(upgrade.Costs);
            return operation;
        }

        public UpgradeOperation GetUpgradeStatus(Building building)
        {
            return m_upgradeManager.GetUpgradeStatus(building);
        }

        public bool TryPauseUpgrade(Building building, out UpgradeOperation operation)
        {
            operation = m_upgradeManager.GetUpgradeStatus(building);
            return m_upgradeManager.PauseUpgrade(building);
        }

        public bool ResumeUpgrade(Building building)
        {
            return m_upgradeManager.ResumeUpgrade(building);
        }

        public UpgradeService(IUpgradeManager upgradeManager, ITileBuildingFactory tileBuildingFactory, IResourceRepository resourceRepository)
        {
            m_upgradeManager = upgradeManager ?? throw new ArgumentNullException(nameof(upgradeManager));
            m_factory = tileBuildingFactory ?? throw new ArgumentNullException(nameof(tileBuildingFactory));
            m_resourceRepository = resourceRepository ?? throw new ArgumentNullException(nameof(resourceRepository));
        }

    }
}

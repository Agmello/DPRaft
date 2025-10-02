using Core.Modules.Buildings.Application.Services;
using Core.Modules.Buildings.Domain;
using Core.Modules.Resources.Application.Dtos;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Application.Contracts
{
    public interface IUpgradeManager
    {
        public Action<UpgradeOperation> OnUpgradeComplete { get; }
        public void ProcessUpgrades();
        public bool AbortUpgrade(Building building);
        public bool TryAbortUpgrade(Building building, out UpgradeOperation operation);
        public UpgradeOperation StartUpgrade(UpgradeOperation operation);
        public UpgradeOperation GetUpgradeStatus(Building building);
        public bool PauseUpgrade(Building building);
        public bool ResumeUpgrade(Building building);

    }
}

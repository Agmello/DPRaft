using Core.Modules.Buildings.Application.Services;
using Core.Modules.Buildings.Domain;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Application.Contracts
{
    internal interface IUpgradeService
    {
        bool AbortUpgrade(Building building);
        bool TryAbortUpgrade(Building building, out UpgradeOperation operation);
        UpgradeOperation StartUpgrade(Tile tile,Building building, int upgrade);
        UpgradeOperation GetUpgradeStatus(Building building);
        bool TryPauseUpgrade(Building building, out UpgradeOperation operation);
        bool ResumeUpgrade(Building building);

    }
}

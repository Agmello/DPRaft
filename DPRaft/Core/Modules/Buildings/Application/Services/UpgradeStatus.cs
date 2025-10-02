using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Application.Services
{
    public enum UpgradeStatus
    {
        None,
        Upgrading,
        Stopped,
        Aborted,
        Done,
        Failed
    }
}

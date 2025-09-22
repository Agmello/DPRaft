using Core.Modules.Resources.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Domain
{
    public class UpgradeInfo
    {
        public string Name { get; }
        public List<ResourceDto> Costs { get; } = new();
        public int TicksToComplete { get; }
    }
}

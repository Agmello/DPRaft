using Core.Domains.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains.Buildings.Actions
{
    public class Upgrade
    {
        public string Name { get; }
        public List<ResourceDto> Costs { get; } = new();
        public int TicksToComplete { get; }
    }
}

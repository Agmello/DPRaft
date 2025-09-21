using Core.Abstractions;
using Core.Domains.Buildings.Actions;
using Core.Domains.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains.Buildings
{
    internal class Farm : Building, IYield
    {
        private static string m_name = "Farm";
        public override string Name => m_name;

        protected override Upgrade[] Upgrades => [
            ];

        private double YieldModifier() => 1.0;
        public IEnumerable<ResourceDto> Get()
        {
            return [new ResourceDto("Food", (1.0 * YieldModifier()))];
        }
    }
}

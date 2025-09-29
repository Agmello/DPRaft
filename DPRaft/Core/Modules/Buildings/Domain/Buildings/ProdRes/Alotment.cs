using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Domain.Buildings.ProdRes
{
    internal class Alotment : ResourceBuilding, IResidentialBuilding
    {
        public override string Name => "Alotment";

        public int Capacity => 1;

        protected override ResourceYield[] m_productions => [
                new ResourceYield("Food", 1)
            ];

        protected override UpgradeInfo[] Upgrades => [];
    }
}

using Core.Modules.Resources.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Domain.Contracts
{
    internal interface IYieldBuilding
    {
        protected ResourceYield[] m_productions { get; }
    }
}

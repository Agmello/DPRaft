using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Domain
{
    public abstract class ProductionBuilding : Building, IProducer
    {
        protected abstract ResourceDto[] m_productions { get; }
        public abstract IEnumerable<ResourceDto> CreateProduction();
        public abstract IEnumerable<ResourceDto> UseResources();
    }
}

using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Domain.Buildings
{
    internal abstract class ResprodBuilding : Building, IProducer, IResidentialBuilding
    {
        public override string Name => throw new NotImplementedException();

        public int Capacity => throw new NotImplementedException();

        public IEnumerable<ResourceDto> CreateProduction()
        {
            throw new NotImplementedException();
        }
    }
}

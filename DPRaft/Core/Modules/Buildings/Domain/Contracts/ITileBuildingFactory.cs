using Core.Domain.Buildings;
using Information.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Domain.Contracts
{
    public interface ITileBuildingFactory : IFactory
    {
        public Building Create(string key);
    }
}

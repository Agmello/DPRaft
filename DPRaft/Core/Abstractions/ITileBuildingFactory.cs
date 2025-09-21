using Core.Domains.Buildings;
using Information.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface ITileBuildingFactory : IFactory
    {
        public Building Create(string key);
    }
}

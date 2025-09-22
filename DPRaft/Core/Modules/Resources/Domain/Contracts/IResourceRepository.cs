using Core.Domain.Buildings;
using Core.Modules.Buildings.Domain;
using Information.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Resources.Domain.Contracts
{
    public interface IResourceRepository : ISingleton
    {
        public void AddBuilding(Building building);
        public void AddResources(string resource, int value);
    }
}

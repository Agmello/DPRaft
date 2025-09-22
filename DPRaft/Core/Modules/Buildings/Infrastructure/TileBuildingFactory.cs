using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Infrastructure
{
    internal class TileBuildingFactory : ITileBuildingFactory
    {
        public Building Create(string key) => key switch
        {
            //"House" => new House(),
            //"Farm" => new Farm(),
            //"LumberMill" => new LumberMill(),
            //"Quarry" => new Quarry(),
            //"Mine" => new Mine(),
            //"Barracks" => new Barracks(),
            _ => throw new ArgumentException($"Unknown building key: {key}")
        };
    }
}

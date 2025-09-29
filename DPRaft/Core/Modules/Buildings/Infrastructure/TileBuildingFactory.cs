using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Buildings.ProdRes;
using Core.Modules.Buildings.Domain.Buildings.Production;
using Core.Modules.Buildings.Domain.Buildings.Residential;
using Core.Modules.Buildings.Domain.Contracts;

namespace Core.Modules.Buildings.Infrastructure
{
    internal class TileBuildingFactory : ITileBuildingFactory
    {
        public Building Create(string key) => key switch
        {
            //"House" => new House(),
            // Wood related buildings
            "Lumber Mill" =>  new LumberMill(),
            "Sawmill" => new Sawmill(),
            // Food related buildings
            "Farm" => new Farm(),
            "Orchard" => new Orchard(),
            "Big Farm" => new BigFarm(),
            "Pasture" => new Pasture(),
            "Pasturage" => new Pasturage(),
            "Ranch" => new Ranch(),
            "Alotment" => new Alotment(),
            // Water related buildings
            "Rain Collector" => new RainCollector(),
            "Gutters" => new Gutters(),
            "Solar Purifiers" => new SolarPurifier(),
            // Stone related buildings
            "Diver" => new Diver(),
            "Wet bell" => new WetBell(),
            "Scuba" => new Scuba(),
            // Housing related buildings
            "Hut" => new Hut(),
            "Cabin" => new Cabin(),
            "Lodge" => new Lodge(),
            _ => throw new ArgumentException($"Unknown building key: {key}")
        };
    }
}

using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Buildings.Domain.Events;
using Core.Modules.Resources.Domain.Contracts;
using Core.Modules.Resources.Domain.Events;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleFrontend
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var host = DependencyInjection.AddConsoleFrontend();

            var observer = host.Services.GetService<IEventObserver>();
            observer.SubscribeSafe<ResourcesChangedEvent>(ResourceChanged);

            var resourceRepository = host.Services.GetService<IResourceRepository>();
            resourceRepository.AddResources("Wood", 25d);

            observer.SubscribeSafe<BuildingChangedEvent>(Building);
            observer.SubscribeSafe<ResourceBuildingEvent>(YieldBuilding);

            var buildingRepository = host.Services.GetService<IBuildingRepository>();
            var tileFactory = host.Services.GetService<ITileBuildingFactory>();
            buildingRepository.AddBuilding(new Core.Modules.Tiles.Domain.Tile(), tileFactory.Create("Farm"));

        }

        static void ResourceChanged(ResourcesChangedEvent @event){

            string resource = $"Resource: {@event.Resource} changed to {@event.NewAmount} (Change: {@event.AmountChanged})";
            Console.WriteLine(resource);

        }
        static void Building(BuildingChangedEvent @event)
        {
            Console.WriteLine($"Building event: {@event.Building.Name}");
        }
        static void YieldBuilding(ResourceBuildingEvent @event)
        {
            Console.WriteLine($"Yield Building event: {@event.Building}");
        }
    }
}

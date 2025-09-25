using Core.BuildingBlocks.Messaging.Observer;
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


        }

        static void ResourceChanged(ResourcesChangedEvent @event){

            string resource = $"Resource: {@event.Resource} changed to {@event.NewAmount} (Change: {@event.AmountChanged})";
            Console.WriteLine(resource);

        }
    }
}

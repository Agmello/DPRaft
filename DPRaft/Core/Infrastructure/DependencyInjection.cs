using Core.BuildingBlocks.Messaging.Observer;
using Core.Infrastructure.Logger;
using Core.Infrastructure.Logger.Contract;
using Core.Infrastructure.Messaging.Observer;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Infrastructure;
using Core.Modules.Resources.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddCoreInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IEventObserver, EventObserver>();
            services.AddSingleton<IEventPublisher, EventPublisher>();
            services.AddSingleton<IEventDispatcher>(x => x.GetService<IEventObserver>() as EventObserver);
            services.AddSingleton<ILogger, Logger.Logger>();
            ResourceDependencyInjection.AddModule(services);
            BuildingsDependencyInjection.AddModule(services);
        }
        
    }
}

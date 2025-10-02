using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Application.Contracts;
using Core.Modules.Buildings.Application.Services;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Buildings.Domain.Events;
using Core.Modules.Buildings.Infrastructure;
using Core.Modules.Resources.Domain.Contracts;
using Core.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CoreTests.Modules.Buildings.UpgradeTests.UpgradeEventTests
{
    public abstract class UpgradeEventTestsBase : UpgradeTestsBase
    {
        Queue<IEvent> RecievedEvents { get; } = new();
        protected IBuildingRepository m_repository;
        protected UpgradeEventTestsBase()
        {

        }
        protected abstract void EventCheckCallback(IEvent @event);
        protected void CheckEvent()
        {
            var @event = RecievedEvents.Dequeue();
            EventCheckCallback(@event);
        }
        protected void AssertNoMoreEvents()
        {
            Assert.Empty(RecievedEvents);
        }
        private void EventReciever(BuildingChangedEvent @event)
        {
            RecievedEvents.Enqueue(@event);
        }
        protected override void SetupBuildingData()
        {
            base.SetupBuildingData();
            m_repository = ServiceProvider.GetRequiredService<IBuildingRepository>();
            var resourceRepository = ServiceProvider.GetRequiredService<IResourceRepository>();
            resourceRepository.Get(Arg.Any<string>()).Returns(100d);
            
            var observer = ServiceProvider.GetRequiredService<IEventObserver>();
            observer.SubscribeSafe<BuildingChangedEvent>(EventReciever);
        }
        protected override void SetupServices(IServiceCollection services)
        {
            SetupEventHandlers(services);
            base.SetupServices(services);
            services.AddSingleton<IUpgradeService, UpgradeService>();
            services.AddSingleton<IBuildingRepository, BuildingRepository>();
            services.AddSingleton(Substitute.For<IResourceRepository>());
        }
    }
}

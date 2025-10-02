using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Buildings.Domain.Events;
using Core.Modules.Resources.Domain.Contracts;
using Core.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CoreTests.Modules.Buildings.ConstructionTests.ConstructionEventTests
{
    public abstract class ConstructionEventTestBase : ConstructionTestsBase
    {
        Queue<IEvent> RecievedEvents { get; } = new();
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
            var resourceRepository = ServiceProvider.GetRequiredService<IResourceRepository>();
            resourceRepository.Get(Arg.Any<string>()).Returns(100d);

            var observer = ServiceProvider.GetRequiredService<IEventObserver>();
            observer.SubscribeSafe<BuildingChangedEvent>(EventReciever);
        }
    }
}

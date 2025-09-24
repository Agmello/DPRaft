using Core.BuildingBlocks.Messaging.Observer;
using Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.Observer
{
    internal class EventPublisher : IEventPublisher
    {
        IEventDispatcher m_eventDispatcher;

        public EventPublisher(IEventDispatcher eventDispatcher)
        {
            m_eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
        }

        public void Publish<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            m_eventDispatcher.Dispatch(@event);
        }
    }
}

using Core.BuildingBlocks.Messaging.Observer;
using Core.SharedKernel;

namespace Core.Infrastructure.Messaging.Observer
{
    public class EventPublisher : IEventPublisher
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
        public void Publish(Type type, IEvent @event)
        {
            m_eventDispatcher.Dispatch(type, @event);
        }
    }
}

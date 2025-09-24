using Core.SharedKernel;

namespace Core.Infrastructure.Messaging
{
    internal class EventPublisher
    {
        private readonly Queue<IEvent> _domainEvents = new();
        private readonly EventDispatcher _eventDispatcher;
        private Task _dispatchingTask = Task.CompletedTask;
        public EventPublisher(EventDispatcher eventDispatcher)
        {
            _eventDispatcher = eventDispatcher ?? throw new ArgumentNullException(nameof(eventDispatcher));
        }
        public IReadOnlyCollection<IEvent> DomainEvents => _domainEvents;
        public void Raise(IEvent @event)
        {
            _domainEvents.Enqueue(@event);
            if( _dispatchingTask.IsCompleted)
            {
                _dispatchingTask = Task.Run(DispatchNext).ContinueWith(CheckQueue);
            }
        }

        private async Task CheckQueue(object o)
        {
            if (_domainEvents.Any())
            {
                await _dispatchingTask.ContinueWith(DispatchNext).ContinueWith(CheckQueue);
            }
        }
        private Task DispatchNext(object o) => DispatchNext();
        private async Task DispatchNext()
        {
            if (_domainEvents.Any())
            {
                var domainEvent = _domainEvents.Dequeue();
                await _eventDispatcher.DispatchAsync(domainEvent);
            }
        }
    }
}

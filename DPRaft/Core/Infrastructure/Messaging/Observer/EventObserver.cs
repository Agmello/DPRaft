using Core.BuildingBlocks.Messaging.Observer;
using Core.SharedKernel;
using System.Collections.Concurrent;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Core.Infrastructure.Messaging.Observer
{
    internal class EventObserver : IEventObserver, IEventDispatcher
    {
        private readonly ConcurrentDictionary<Type, ISubjectSink> m_subjects = new();
        public IDisposable SubscribeSafe<TEvent>(Action<TEvent> onNext) where TEvent : class, IEvent
        {
            return GetEvent<TEvent>().SubscribeSafe(new AnonymousObserver<TEvent>(onNext));
        }

        public IObservable<TEvent> GetEvent<TEvent>() where TEvent : class, IEvent
        {
            var subject = m_subjects.GetOrAdd(typeof(TEvent), _ => new SubjectSink<TEvent>(new Subject<TEvent>()));
            return subject.AsObservable<TEvent>().AsObservable();
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : class, IEvent
            => DispatchSubjects(typeof(TEvent), @event);
        public void Dispatch(Type type, IEvent @event)
            => DispatchSubjects(type, @event);
        private void DispatchSubjects(Type type, IEvent @event)
        {
            m_subjects.Where(kv => kv.Key.IsAssignableFrom(type))
                .Select(kv => kv.Value)
                  .ToList()
                  .ForEach(f => f.OnNext(@event));
        }
    }
}

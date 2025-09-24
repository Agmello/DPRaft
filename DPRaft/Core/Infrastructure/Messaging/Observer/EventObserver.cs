using Core.BuildingBlocks.Messaging.Observer;
using Core.SharedKernel;
using System.Reactive.Subjects;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using System.Reactive;

namespace Core.Infrastructure.Messaging.Observer
{
    internal class EventObserver : IEventObserver, IEventDispatcher
    {
        private readonly ConcurrentDictionary<Type, object> m_subjects = new();
        public IDisposable SubscribeSafe<TEvent>(Action<TEvent> onNext) where TEvent : class, IEvent
        {
            return GetEvent<TEvent>().SubscribeSafe(new AnonymousObserver<TEvent>(onNext));
        }

        public IObservable<TEvent> GetEvent<TEvent>() where TEvent : class, IEvent
        {
            var subject = (ISubject<TEvent>)m_subjects.GetOrAdd(typeof(TEvent), _ => new System.Reactive.Subjects.Subject<TEvent>());
            return subject.AsObservable();
        }

        public void Dispatch<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            var subject = (ISubject<TEvent>)m_subjects.GetOrAdd(typeof(TEvent), _ => new System.Reactive.Subjects.Subject<TEvent>());
            subject.OnNext(@event);
        }
    }
}

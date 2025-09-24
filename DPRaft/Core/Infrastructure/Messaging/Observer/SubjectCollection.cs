using Core.BuildingBlocks.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.Observer
{
    public class SubjectCollection : ISubjectCollection
    {
        Dictionary<Type, IInternalSubject> m_observers = new();
        public SubjectCollection() { }

        public void Attach<TType>(BuildingBlocks.Messaging.IObserver<TType> observer) where TType : class
        {
            if(m_observers.TryGetValue(typeof(TType), out var subject))
                if(subject is ISubject<TType> typedSubject)
                    typedSubject.Attach(observer);
                else
                    throw new InvalidOperationException($"Subject registered for {typeof(TType).FullName} is not of expected type.");
            else
            {
                var newSubject = new Subject<TType>();
                newSubject.Attach(observer);
                m_observers[typeof(TType)] = newSubject;
            }
        }
        public void Detach<TType>(BuildingBlocks.Messaging.IObserver<TType> observer) where TType : class
        {
            if (m_observers.TryGetValue(typeof(TType), out var subject))
                if (subject is ISubject<TType> typedSubject)
                    typedSubject.Detach(observer);
                else
                    throw new InvalidOperationException($"Subject registered for {typeof(TType).FullName} is not of expected type.");
        }
        public void Notify<TType>(TType args) where TType : class
        {
            if (m_observers.TryGetValue(typeof(TType), out var subject))
                if (subject is ISubject<TType> typedSubject)
                    typedSubject.Notify(args);
                else
                    throw new InvalidOperationException($"Subject registered for {typeof(TType).FullName} is not of expected type.");
        }

        public void Attach<TType>(IObserver observer) where TType : class
        {
            if(m_observers.TryGetValue(typeof(TType), out var subject))
                if(subject is ISubject typedSubject)
                    typedSubject.Attach(observer);
                else
                    throw new InvalidOperationException($"Subject registered for {typeof(TType).FullName} is not of expected type.");
            else
            {
                var newSubject = new Subject();
                newSubject.Attach(observer);
                m_observers[typeof(TType)] = newSubject;
            }
        }
        public void Detach<TType>(IObserver observer) where TType : class
        {
            if (m_observers.TryGetValue(typeof(TType), out var subject))
                if (subject is ISubject typedSubject)
                    typedSubject.Detach(observer);
                else
                    throw new InvalidOperationException($"Subject registered for {typeof(TType).FullName} is not of expected type.");
        }
        public void Notify<TType>() where TType : class
        {
            if (m_observers.TryGetValue(typeof(TType), out var subject))
                if (subject is ISubject typedSubject)
                    typedSubject.Notify();
                else
                    throw new InvalidOperationException($"Subject registered for {typeof(TType).FullName} is not of expected type.");
        }

    }
}

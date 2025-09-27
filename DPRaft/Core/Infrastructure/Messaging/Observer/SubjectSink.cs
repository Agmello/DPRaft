using Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging.Observer
{
    public interface ISubjectSink
    {
        void OnNext(IEvent @event);
        IObservable<TEvent> AsObservable<TEvent>() where TEvent : class, IEvent;

    }

    public class SubjectSink<TEvent> : ISubjectSink where TEvent : class, IEvent
    {
        public ISubject<TEvent> m_subject { get; }
        public SubjectSink(ISubject<TEvent> subject)
        {
            m_subject = subject;
        }
        public void OnNext(IEvent e)
        {
            if(e is TEvent ev)
                m_subject.OnNext(ev);
        }
        public IObservable<TEvent> AsObservable<TEvent>() where TEvent : class, IEvent
            => (m_subject as IObservable<TEvent>);// ?? m_subject.OfType<TEvent2>();
    }
}

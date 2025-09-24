using Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BuildingBlocks.Messaging.Observer
{
    internal interface IEventObserver
    {
        IObservable<TEvent> GetEvent<TEvent>() where TEvent : class, IEvent;
        IDisposable SubscribeSafe<TEvent>(Action<TEvent> onNext) where TEvent : class, IEvent;
    }
}

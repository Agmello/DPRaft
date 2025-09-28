using Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BuildingBlocks.Messaging.Observer
{
    public interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : class, IEvent;
        void Publish(Type type, IEvent @event);
    }
}

using Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BuildingBlocks.Messaging.Observer
{
    internal interface IEventPublisher
    {
        void Publish<TEvent>(TEvent @event) where TEvent : class, IEvent;
    }
}

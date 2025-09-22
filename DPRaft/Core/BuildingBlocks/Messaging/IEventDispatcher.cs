using Core.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BuildingBlocks.Messaging
{
    public interface IEventDispatcher
    {
        Task DispatchAsync(IEnumerable<IEvent> events, CancellationToken ct = default);
        Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default) where TEvent : IEvent;
    }
}

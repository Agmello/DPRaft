using Core.BuildingBlocks.Messaging;
using Core.SharedKernel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Messaging
{
    internal class  EventDispatcher : IEventDispatcher
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private static readonly ConcurrentDictionary<Type, Func<IServiceProvider, IEvent, CancellationToken, Task>> _invokers = new();

        public EventDispatcher(IServiceScopeFactory scopeFactory) => _scopeFactory = scopeFactory;

        public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken ct = default)
            where TEvent : IEvent
        {
            using var scope = _scopeFactory.CreateScope();
            var handlers = scope.ServiceProvider.GetServices<IEventHandler<TEvent>>();
            // sequential; switch to Task.WhenAll(...) if you want parallel (mind ordering!)
            foreach (var h in handlers)
                await h.HandleAsync(@event, ct);
        }

        public async Task DispatchAsync(IEnumerable<IEvent> events, CancellationToken ct = default)
        {
            foreach (var e in events)
            {
                var invoker = _invokers.GetOrAdd(e.GetType(), BuildInvoker);
                using var scope = _scopeFactory.CreateScope();
                await invoker(scope.ServiceProvider, e, ct);
            }
        }
        public async Task DispatchAsync(IEvent @event, CancellationToken ct = default)
        {
            var invoker = _invokers.GetOrAdd(@event.GetType(), BuildInvoker);
            using var scope = _scopeFactory.CreateScope();
            await invoker(scope.ServiceProvider, @event, ct);
        }
        // builds a closed generic invoker: (sp, e, ct) => foreach (h in sp.GetServices<IEventHandler<T>>()) h.HandleAsync((T)e, ct)
        private static Func<IServiceProvider, IEvent, CancellationToken, Task> BuildInvoker(Type eventType)
        {
            var mi = typeof(EventDispatcher).GetMethod(nameof(InvokeHandlers), BindingFlags.NonPublic | BindingFlags.Static)!
                                              .MakeGenericMethod(eventType);
            return (sp, e, ct) => (Task)mi.Invoke(null, new object[] { sp, e, ct })!;
        }

        private static async Task InvokeHandlers<TEvent>(IServiceProvider sp, IEvent e, CancellationToken ct)
            where TEvent : IEvent
        {
            foreach (var h in sp.GetServices<IEventHandler<TEvent>>())
                await h.HandleAsync((TEvent)e, ct);
        }
    }
}

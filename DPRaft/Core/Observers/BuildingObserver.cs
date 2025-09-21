using Core.Abstractions;
using Core.Domains.Buildings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Observers
{
    internal class BuildingObserver
    {
        Dictionary<Type, List<Action<IEvent>>> m_listeners = new();
    
        internal void Subscribe<T>(Action<IEvent> listener) where T : IEvent
        {
            var type = typeof(T);
            if (!m_listeners.ContainsKey(type))
                m_listeners[type] = new List<Action<IEvent>>();
            if (!m_listeners[type].Contains(listener))
                m_listeners[type].Add(listener);
        }
        internal void Unsubscribe<T>(Action<IEvent> listener) where T : IEvent
        {
            var type = typeof(T);
            if (!m_listeners.ContainsKey(type))
                return;
            if (m_listeners[type].Contains(listener))
                m_listeners[type].Remove(listener);
        }
        internal void Notify<T>(T eventData) where T : IEvent
        {
            var type = eventData.GetType();
            if (!m_listeners.ContainsKey(type))
                return;
            foreach(var listener in m_listeners[type])
            {
                listener(eventData);
            }
        }
    }
}

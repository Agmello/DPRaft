using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Domain;
using Core.Modules.Game.Domain.Banks;
using Core.Modules.Resources.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Resources.Domain
{
    internal class ResourceBank : BankBase
    {
        private Dictionary<string, double> m_bank = new();

        internal ResourceBank(Guid key, IEventPublisher publisher) : base(key, publisher)
        {
        }
        internal double Get(Guid key, string resource)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            return m_bank.ContainsKey(resource) ? m_bank[resource] : 0;
        }
        internal IReadOnlyDictionary<string, double> GetAll(Guid key)
        {
            return hasAccess(key) ? m_bank :
                throw new ArgumentNullException(nameof(key));
        }
        internal double AddResources(Guid key, string resource, double value)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            if (m_bank.ContainsKey(resource))
                m_bank[resource] += value;
            else
                m_bank.Add(resource, value);
            NotifyResourceChange(resource, value);
            return m_bank[resource];
        }
        internal double RemoveResources(Guid key, string resource, double value)
        {
            if(!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            if (m_bank.ContainsKey(resource))
                m_bank[resource] -= value;
            else
                m_bank.Add(resource, -value);
            NotifyResourceChange(resource, -value);
            return m_bank[resource];
        }

        private void NotifyResourceChange(string resource, double amount)
        {
            var @event = new ResourcesChangedEvent(resource, m_bank[resource], amount);
            m_eventPublisher.Publish(@event);
        }
    }
}

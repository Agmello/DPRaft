using Core.Modules.Buildings.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Game.Domain
{
    internal class ResourceBank
    {
        private Dictionary<string, double> m_bank = new();
        private Guid m_key;
        private bool hasAccess(Guid key) => m_key.Equals(key);
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

            return m_bank[resource];
        }
    }
}

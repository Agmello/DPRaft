using Core.Domains.Buildings;
using Information.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains.Resources
{
    public class ResourceBank : ISingleton
    {
        private Dictionary<string, int> m_bank = new();
        private List<Building> m_buildings = new();
        private Guid m_key;

        private bool hasAccess(Guid key) => m_key.Equals(key);
        internal Dictionary<string, int> Bank(Guid key) {
            return hasAccess(key) ? m_bank :
                throw new ArgumentNullException(nameof(key));
        }
        internal List<Building> Buildings(Guid key)
        {
            return hasAccess(key) ? m_buildings :
                throw new ArgumentNullException(nameof(key));
        }

    }
}

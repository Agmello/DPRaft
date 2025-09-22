using Core.Modules.Buildings.Domain;
using Core.Modules.Game.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Game.Domain
{
    internal class GameSession : IResourceHandler, IHumanHandler, IBuildingHandler
    {
        private List<Building> m_buildings = new();
        private Guid m_key;
        private ResourceBank m_bank = new();
        private bool hasAccess(Guid key) => m_key.Equals(key);
        internal IReadOnlyDictionary<string, double> Resources(Guid key)
        {
            return m_bank.GetAll(key);
        }
        internal List<Building> Buildings(Guid key)
        {
            return hasAccess(key) ? m_buildings :
                throw new ArgumentNullException(nameof(key));
        }

        IEnumerable<(string Resource, double Amount)> IResourceHandler.Resources(Guid key)
        {
            return m_bank.GetAll(key).Select(x => (x.Key,x.Value)).ToList();
        }

        public double AddResources(Guid key, string resource, double value)
        {
            return m_bank.AddResources(key, resource, value);
        }

        public double RemoveResources(Guid key, string resource, double value)
        {
            return m_bank.RemoveResources(key, resource, value);
        }
    }
}

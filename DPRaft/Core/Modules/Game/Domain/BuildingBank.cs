using Core.BuildingBlocks.Messaging;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Game.Domain
{
    internal class BuildingBank : BankBase
    {
        Dictionary<Tile, Building> m_bank = new();
        ITileBuildingFactory m_factory;
        internal BuildingBank(Guid key, ISubjectCollection subjectCollection) : base(key)
        {
            
        }

        internal IReadOnlyDictionary<Tile, Building> GetAll(Guid key)
        {
            return hasAccess(key) ? m_bank :
                throw new ArgumentNullException(nameof(key));
        }
        internal Building AddBuilding(Guid key, Tile tile, Building building)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            m_bank[tile] = building;
            return m_bank[tile];
        }
        internal Building RemoveBuilding(Guid key, Tile tile)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            if (m_bank.ContainsKey(tile))
            {
                var building = m_bank[tile];
                m_bank.Remove(tile);
                return building;
            }
            return null;
        }
        internal Building UpgradeBuilding(Guid key, Tile tile, Building building)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            if (m_bank.ContainsKey(tile))
            {
                var oldBuilding = m_bank[tile];
                m_bank[tile] = building;
                return building;
            }
            return null;
        }
        internal void DestroyTile(Guid key, Tile tile)
        {
            if (!hasAccess(key))
                throw new ArgumentNullException(nameof(key));
            m_bank.Remove(tile);
        }
    }
}

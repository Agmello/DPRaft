using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Tiles.Domain.Contracts;
using Core.Modules.Tiles.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Tiles.Domain
{
    internal class TileMap : ITileMap
    {
        private readonly IBuildingRepository m_buildingRepository;
        private readonly IEventPublisher m_publisher;
        public TileMap(IBuildingRepository buildingRepository, IEventPublisher publisher) {
            m_buildingRepository = buildingRepository;
            m_publisher = publisher;
        }
        private Dictionary<(int x, int y), Tile> m_tiles = new Dictionary<(int x, int y), Tile>();

        public Tile? GetTile(int x, int y)
        {
            if (m_tiles.TryGetValue((x, y), out var tile))
                return tile;
            return null;
        }
        public IEnumerable<Tile> GetAllTiles()
        {
            return m_tiles.Values;
        }
        public void AddTile(Tile tile)
        {
            if (tile == null)
                throw new ArgumentNullException(nameof(tile));
            m_tiles[(tile.X, tile.Y)] = tile;
            m_buildingRepository.AddBuildingSpot(tile);
            m_publisher.Publish(new TileEvent(tile, Buildings.Domain.Events.ChangeType.Added));
        }
        public void RemoveTile(Tile tile)
        {
            if (tile == null)
                throw new ArgumentNullException(nameof(tile));
            if (m_tiles.Remove((tile.X, tile.Y)))
            {
                m_buildingRepository.RemoveBuildingSpot(tile);
                m_publisher.Publish(new TileEvent(tile, Buildings.Domain.Events.ChangeType.Removed));
            }
        }
    }
}

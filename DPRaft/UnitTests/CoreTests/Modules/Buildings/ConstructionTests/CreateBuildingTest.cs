using Core.Modules.Buildings.Domain;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CoreTests.Modules.Buildings.ConstructionTests
{
    public class CreateBuildingTest : ConstructionTestsBase
    {
        [Fact]
        public void CreateBuilding()
        {
            var tile = new Tile();
            var farm = m_factory.Create("Farm");
            m_repository.AddBuilding(tile, farm);

            var buildings = m_repository.GetAllBuildings();
            AssertBuildingExist(farm);
            Assert.False(buildings.Any(y => y != farm));
        }
        [Fact]
        public void RemoveBuilding()
        {
            var tile = new Tile();
            var farm = m_factory.Create("Farm");
            m_repository.AddBuilding(tile, farm);
            AssertBuildingExist(farm);
            m_repository.DestroyBuilding(tile);
            AssertBuildingDontExist(farm);
            AssertEmpty(tile);
        }
        [Fact]
        public void DestroyTile()
        {
            var tile = new Tile();
            var farm = m_factory.Create("Farm");
            m_repository.AddBuilding(tile, farm);
            AssertBuildingExist(farm);
            m_repository.RemoveBuildingSpot(tile);
            AssertBuildingDontExist(farm);
            var buidling = m_repository.GetBuilding(tile); // Should not throw exception
            Assert.Null(buidling);

        }
    

    }
}

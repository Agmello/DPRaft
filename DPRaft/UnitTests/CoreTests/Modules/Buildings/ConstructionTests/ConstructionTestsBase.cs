using Core.Modules.Buildings.Application.Contracts;
using Core.Modules.Buildings.Application.Services;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Buildings;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Buildings.Infrastructure;
using Core.Modules.Resources.Domain.Contracts;
using Core.Modules.Tiles.Domain;
using Information.Patterns;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CoreTests.Modules.Buildings.ConstructionTests
{
    public abstract class ConstructionTestsBase : BuildingTestBase
    {
        protected ITileBuildingFactory m_factory;
        protected IBuildingRepository m_repository;
        protected void AssertBuildingExist(Building building)
        {
            var buildings = m_repository.GetAllBuildings();
            Assert.Contains(building, buildings);
        }
        protected void AssertBuildingDontExist(Building building)
        {
            var buildings = m_repository.GetAllBuildings();
            Assert.DoesNotContain(building, buildings);
        }
        protected void AssertEmpty(Tile tile)
        {
            var buildings = m_repository.GetBuilding(tile);
            Assert.IsType<EmptyBuilding>(buildings);
        }
        protected override void SetupBuildingData()
        {
            // No building data needed
            m_factory = ServiceProvider.GetService<ITileBuildingFactory>();
            m_repository = ServiceProvider.GetService<IBuildingRepository>();
        }
        protected override void SetupServices(IServiceCollection services)
        {
            services.AddSingleton<ITileBuildingFactory, TileBuildingFactory>();
            services.AddSingleton(Substitute.For<IUpgradeService>());
            services.AddSingleton(Substitute.For<IResourceRepository>());
            services.AddSingleton<IUpgradeManager, UpgradeManager>();
            services.AddSingleton<IBuildingRepository, BuildingRepository>();

            SetupEventHandlers(services);
            //base.SetupServices(services);
            //services.Replace(ServiceDescriptor.Singleton<IBuildingRepository,Substitute.For<IBuildingRepository>());
            //services.AddSingleton<IBuildingRepository>(Substitute.For<IBuildingRepository>());
            //services.AddSingleton(Substitute.For<IResourceRepository>());
        }
    }
}

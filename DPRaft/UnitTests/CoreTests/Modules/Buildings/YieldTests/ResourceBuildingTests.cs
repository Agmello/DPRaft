using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.CoreTests.Modules.Buildings.YieldTests
{
    public class ResourceBuildingTests : BuildingTestBase
    {
        protected override bool InitBuildings => true;
        [Fact]
        public void VerifyProducerBuilding()
        {
            foreach (var building in AllBuilding)
            {
                if(building is IProducer pb)
                {
                    Assert.NotEmpty(pb.CreateProduction());
                }
            }
        }
        [Fact]
        public void VerifyConsumerBuilding()
        {
            foreach (var building in AllBuilding)
            {
                if (building is IConsumer pb)
                {
                    Assert.NotEmpty(pb.CreateConsumption());
                }
            }
        }
        [Fact]
        public void VerifyYieldBuilding()
        {
            foreach (var building in AllBuilding)
            {
                if (building is IYield pb)
                {
                    var consumption = pb.CreateConsumption();
                    var producing = pb.CreateProduction();
                    Assert.NotEmpty(consumption);
                    Assert.NotEmpty(producing);

                    var yield = pb.CreateYield();
                    Assert.NotEmpty(yield);
                    
                    var yields = new List<ResourceDto>();
                    yields.AddRange(
                            producing
                            .GroupBy(x => x.Key)
                            .Select(y => 
                                new ResourceDto(y.Key, y.Sum(z => z.Amount))
                            ));
                    foreach(var item in yields)
                    {
                        var cons = consumption.Where(x => x.Key == item.Key).Sum(y => y.Amount);
                        item.Amount -= cons;
                    }
                    yields.AddRange(consumption.Where(x => !yields.Contains(x)).Select(y => new ResourceDto(y.Key,-y.Amount)));


                    foreach(var item in yields)
                    {
                        var y = yield.FirstOrDefault(x => x.Key == item.Key);
                        Console.WriteLine($"Resource <{y.Key}>: y:{y.Amount} item:{item.Amount}");
                        Assert.NotNull(y);
                        Assert.Equal(item.Amount, y.Amount);
                    }

                }
            }
        }

        protected override void SetupBuildingData()
        {
        }
    }
}

using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;
using Core.Modules.Resources.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Domain.Buildings
{
    public abstract class ResourceBuilding : Building
    {
        protected abstract ResourceYield[] m_productions { get; }
        public int Setting { get; set; } = 0;
        public virtual IEnumerable<ResourceDto> CreateProduction() =>
                        m_productions
                         .Where(p => !p.Consume)
                         .Select(p => new ResourceDto(p.ResourceName, p.Amount));
        public virtual IEnumerable<ResourceDto> CreateConsumption() => 
                        m_productions
                         .Where(p => p.Consume)
                         .Select(p => new ResourceDto(p.ResourceName, p.Amount));
        public virtual IEnumerable<ResourceDto> CreateYield() =>
                        m_productions
                            .Where(MatchSettings)
                            .GroupBy(x => x.ResourceName)
                            .Select(y => 
                                new ResourceDto (
                                    y.Key,
                                    y.Sum(z =>  z.Consume ? -z.Amount : z.Amount)
                                    )
                            );
        public void Pause() => Setting = -1;
        public bool IsPaused() => Setting == -1;
        private bool MatchSettings(ResourceYield yield)
        {
            return Setting == 0 || yield.Setting == Setting || yield.Setting == 0;
        }
    }
}

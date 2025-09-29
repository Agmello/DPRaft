using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Infrastructure;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Application.Services
{
    internal class UpgradeComposite
    {
        IEventPublisher m_eventPublisher;
        List<UpgradeInformation> m_upgradeStatuses = new();

        public UpgradeComposite(IEventPublisher eventPublisher)
        {
            m_eventPublisher = eventPublisher;
        }
        internal UpgradeInformation Enqueue(Tile tile, Building from, Building to, int time)
        {
            var status = new UpgradeInformation(tile, to, from, time);
            m_upgradeStatuses.Add(status);
            BuildingEventPublisher.Create(m_eventPublisher, tile, from, Domain.Events.ChangeType.Upgrading, to);
            return status;
        }
        internal void Abort(Building building)
        {
            var status = m_upgradeStatuses.FirstOrDefault(x => x.From == building);
            if (status != null)
            {
                
                m_upgradeStatuses.Remove(status);
                status.Status = UpgradeStatus.Aborted;
                BuildingEventPublisher.Create(m_eventPublisher, status.Tile, status.From, Domain.Events.ChangeType.UprgadeStopped);
            }
        }
        internal void ExecuteTick()
        {
            m_upgradeStatuses.ForEach(StepTick);
            var completed = m_upgradeStatuses.Where(x => x.TicksLeft <= 0).ToList();

            foreach(var item in completed)
            {
                m_upgradeStatuses.Remove(item);
                item.Status = UpgradeStatus.Done;
                BuildingEventPublisher.Create(m_eventPublisher, item.Tile, item.From, Domain.Events.ChangeType.Upgraded, item.To);
            }
        }
        private void StepTick(UpgradeInformation info)
        {
            if(info.Status != UpgradeStatus.Upgrading) 
                return;
            info.TicksLeft -= 1;
        }
    }
}

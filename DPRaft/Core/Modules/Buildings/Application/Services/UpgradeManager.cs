using Core.BuildingBlocks.Messaging.Observer;
using Core.Modules.Buildings.Application.Contracts;
using Core.Modules.Buildings.Domain;
using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Buildings.Infrastructure;
using Core.Modules.Resources.Application.Dtos;
using Core.Modules.Tiles.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Buildings.Application.Services
{
    internal class UpgradeManager : IUpgradeManager
    {
        List<UpgradeOperation> m_upgradeStatuses = new();
        public Action<UpgradeOperation> OnUpgradeComplete { get; internal set; }

        public void ProcessUpgrades()
        {
            var upgrading = m_upgradeStatuses.Where(x => x.Status == UpgradeStatus.Upgrading).ToList();
            foreach (var item in upgrading)
                ProcessItem(item);
        }
        private void ProcessItem(UpgradeOperation op)
        {
            op.TicksLeft -= 1;
            if(op.TicksLeft < 1)
            {
                m_upgradeStatuses.Remove(op);
                op.Status = UpgradeStatus.Done;
                OnUpgradeComplete?.Invoke(op);
            }
        }
        public bool AbortUpgrade(Building building)
        {
            return TryAbortUpgrade(building, out var _);
        }

        public UpgradeOperation StartUpgrade(UpgradeOperation operation)
        {
            m_upgradeStatuses.Add(operation);
            return operation;
        }

        public UpgradeOperation? GetUpgradeStatus(Building building)
        {
            return m_upgradeStatuses.FirstOrDefault(x => x.From == building);
        }

        public bool PauseUpgrade(Building building)
        {
            var operation = GetUpgradeStatus(building);
            if (operation == null) return false;

            operation.Status = UpgradeStatus.Stopped;
            return true;
        }

        public bool TryAbortUpgrade(Building building, out UpgradeOperation operation)
        {
            operation = m_upgradeStatuses.FirstOrDefault(x => x.From == building);
            if (operation == null) return false;

            m_upgradeStatuses.Remove(operation);
            operation.TicksLeft = 0;
            operation.Status = UpgradeStatus.Aborted;

            return true;
        }

        public bool ResumeUpgrade(Building building)
        {
            var operation = GetUpgradeStatus(building);
            if (operation == null) return false;
            operation.Status = UpgradeStatus.Upgrading;
            return true;
        }
        /*public UpgradeManager(Action<UpgradeOperation> upgradeComplete)
{
UpgradeComplete = upgradeComplete;
}
internal UpgradeOperation Enqueue(Tile tile, Building from, Building to, int time)
{
var status = new UpgradeOperation(tile, to, from, time);
m_upgradeStatuses.Add(status);
//BuildingEventPublisher.Create(m_eventPublisher, tile, from, Domain.Events.ChangeType.Upgrading, to);
return status;
}
internal UpgradeOperation Abort(Building building)
{
var status = m_upgradeStatuses.FirstOrDefault(x => x.From == building);
if (status != null)
{

m_upgradeStatuses.Remove(status);
status.Status = UpgradeStatus.Aborted;
return status;
//BuildingEventPublisher.Create(m_eventPublisher, status.Tile, status.From, Domain.Events.ChangeType.UpgradeStopped);
}
return null;
}
internal void ExecuteTick()
{
m_upgradeStatuses.ForEach(StepTick);
var completed = m_upgradeStatuses.Where(x => x.TicksLeft <= 0).ToList();

foreach(var item in completed)
{
m_upgradeStatuses.Remove(item);
item.Status = UpgradeStatus.Done;
//BuildingEventPublisher.Create(m_eventPublisher, item.Tile, item.From, Domain.Events.ChangeType.Upgraded, item.To);
}
}
private void StepTick(UpgradeOperation info)
{
if(info.Status != UpgradeStatus.Upgrading) 
return;
info.TicksLeft -= 1;
}*/
    }
}

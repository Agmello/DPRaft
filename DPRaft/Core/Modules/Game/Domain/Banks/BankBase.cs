using Core.BuildingBlocks.Messaging.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Game.Domain.Banks
{
    internal abstract class BankBase
    {
        protected Guid m_key;
        protected IEventPublisher m_eventPublisher;
        protected bool hasAccess(Guid key) => m_key.Equals(key);

        internal BankBase(Guid key, IEventPublisher eventPublisher)
        {
            m_key = key;
            m_eventPublisher = eventPublisher ?? throw new ArgumentNullException(nameof(eventPublisher));
        }
    }
}

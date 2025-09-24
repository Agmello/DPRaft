using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Game.Domain
{
    internal abstract class BankBase
    {
        protected Guid m_key;
        protected bool hasAccess(Guid key) => m_key.Equals(key);

        internal BankBase(Guid key)
        {
            m_key = key;
        }
    }
}

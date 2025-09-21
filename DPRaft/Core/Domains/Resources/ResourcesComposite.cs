using Core.Abstractions;
using Core.Domains.Buildings;
using Information.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Domains.Resources
{
    internal class ResourcesComposite : IYield
    {
        List<IYield> m_yielders = new();
        Dictionary<string, double>  m_yields = new();

        public IEnumerable<ResourceDto> Get(bool recalculate = false)
        {
            if (recalculate)
            {
                m_yields.Clear();
                m_yielders.ForEach(y =>
                {
                    foreach (var r in y.Get())
                    {
                        if (m_yields.ContainsKey(r.Key))
                            m_yields[r.Key] += r.Amount;
                        else
                            m_yields[r.Key] = r.Amount;
                    }
                });
            }
                
            return Get();
        }

        public IEnumerable<ResourceDto> Get()
        {
            return m_yields.Select(kv => new ResourceDto(kv.Key, kv.Value));
        }

        internal void Add(IYield yielder)
        {
            if (!m_yielders.Contains(yielder))
            {
                m_yielders.Add(yielder);
                foreach(var yield in yielder.Get())
                {
                    if (m_yields.ContainsKey(yield.Key))
                        m_yields[yield.Key] += yield.Amount;
                    else
                        m_yields[yield.Key] = yield.Amount;
                };
            }
        }
        internal void Remove(IYield yielder)
        {
            if (!m_yielders.Contains(yielder))
                return;

            m_yielders.Remove(yielder);
            foreach (var yield in yielder.Get())
            {
                if (m_yields.ContainsKey(yield.Key))
                    m_yields[yield.Key] -= yield.Amount;
            };
        }
    }
}

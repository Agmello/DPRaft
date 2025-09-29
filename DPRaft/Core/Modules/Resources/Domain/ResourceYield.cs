using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Resources.Domain
{
    public class ResourceYield
    {
        public string ResourceName { get; }
        public double Amount { get; }
        public bool Consume { get; } = false;
        public int Setting { get; }
        public ResourceYield(string resourceName, double amount, bool consume = false, int setting = 0)
        {
            ResourceName = resourceName;
            Amount = amount;
            Consume = consume;
            Setting = setting;
        }
    }
}

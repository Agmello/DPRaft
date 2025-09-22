using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Game.Domain.Contracts
{
    internal interface IResourceHandler
    {
        IEnumerable<(string Resource, double Amount)> Resources(Guid key);
        double AddResources(Guid key, string resource, double value);
        double RemoveResources(Guid key, string resource, double value);
    }
}

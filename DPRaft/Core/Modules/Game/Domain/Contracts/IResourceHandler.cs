using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Game.Domain.Contracts
{
    internal interface IResourceHandler
    {
        IEnumerable<(string resource, double amount)> Resources();

        // Add and Remove should not be exposed to outside, only the repository should be able to do that
        /*double AddResources(string resource, double value);
        double RemoveResources(string resource, double value);*/
    }
}

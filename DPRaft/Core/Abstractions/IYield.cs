using Core.Domains.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Abstractions
{
    public interface IYield
    {
        public IEnumerable<ResourceDto> Get();
    }
}

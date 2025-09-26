using Core.Modules.Buildings.Domain.Contracts;
using Core.Modules.Resources.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Modules.Resources.Application.Contracts
{
    internal interface IYieldComposite : IYield
    {
        bool TryRegisterYield(IYield yield);
        bool TryUnregisterYield(IYield yield);
        bool TryRegisterProducer(IProducer producer);
        bool TryUnregisterProducer(IProducer producer);
        bool TryRegisterConsumer(IConsumer consumer);
        bool TryUnregisterConsumer(IConsumer consumer);
    }
}

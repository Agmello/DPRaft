using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BuildingBlocks.Messaging
{
    public interface IObserver
    {
        void OnNotify();
    }
    public interface IObserver<TType>
    {
        void OnNotify(TType eventData);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BuildingBlocks.Messaging
{
    public interface IInternalSubject
    {
    }
    public interface ISubject : IInternalSubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
    public interface ISubject<TType> : IInternalSubject where TType : class
    {
        void Attach(IObserver<TType> observer);
        void Detach(IObserver<TType> observer);
        void Notify(TType args);
    }
}

using Core.Infrastructure.Messaging.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BuildingBlocks.Messaging
{
    public interface ISubjectCollection
    {
        public void Attach<TType>(IObserver<TType> observer) where TType : class;
        public void Detach<TType>(IObserver<TType> observer) where TType : class;
        public void Notify<TType>(TType args) where TType : class;

        public void Attach<TType>(IObserver observer) where TType : class;
        public void Detach<TType>(IObserver observer) where TType : class;
        public void Notify<TType>() where TType : class;
    }
}

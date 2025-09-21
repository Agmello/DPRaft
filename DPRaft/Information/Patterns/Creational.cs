using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Information.Patterns
{
    public interface IFactory
    {
    }
    public interface IAbstractFactory
    {
    }
    public interface IBuilder
    {
    }
    public interface IPrototype<T>
    {
        T Clone();
    }
    public interface ISingleton
    {
    }


}

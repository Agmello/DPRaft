using Core.BuildingBlocks.Messaging;

namespace Core.Infrastructure.Messaging.Observer
{
    public class Subject : ISubject
    {
        List<IObserver> m_observers = new();
        public Subject() { }
        public void Attach(IObserver observer)
        {
            if (!m_observers.Contains(observer))
                m_observers.Add(observer);
        }
        public void Detach(IObserver observer)
        {
            if (m_observers.Contains(observer))
                m_observers.Remove(observer);
        }
        public void Notify()
        {
            m_observers.ForEach(o => o.OnNotify());
        }
    }
    public class Subject<TType> : ISubject<TType> where TType : class
    {
        List<BuildingBlocks.Messaging.IObserver<TType>> m_observers = new();
        public Subject() { }

        public void Attach(BuildingBlocks.Messaging.IObserver<TType> observer)
        {
            if (!m_observers.Contains(observer))
                m_observers.Add(observer);
        }
        public void Detach(BuildingBlocks.Messaging.IObserver<TType> observer)
        {
            if (m_observers.Contains(observer))
                m_observers.Remove(observer);
        }
        public void Notify(TType args)
        {
            m_observers.ForEach(o => o.OnNotify(args));
        }
    }
}

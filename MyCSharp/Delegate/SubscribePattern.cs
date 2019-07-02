using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Delegate.NoDelegate
{
    public interface IPublisher
    {
        void RaiseEvent(EventArgs args);

        void Add(ISubscriber subscriber);

        void Remove(ISubscriber subscriber);
    }


    public interface ISubscriber
    {
        void Subscribe(IPublisher publiser);

        void Unsubscribe(IPublisher publiser);

        void HandleEvent(object publiser, EventArgs args);
    }

    public class Publisher : IPublisher
    {
        public readonly List<ISubscriber> subscribers = new List<ISubscriber>();

        public void Add(ISubscriber subscriber)
        {
            subscribers.Add(subscriber);
        }

        public void RaiseEvent(EventArgs args)
        {
            foreach (var item in subscribers)
            {
                item.HandleEvent(this, args);
            }
        }

        public void Remove(ISubscriber subscriber)
        {
            subscribers.Remove(subscriber);
        }
    }

    public class Subscriber : ISubscriber
    {
        public void HandleEvent(object publiser, EventArgs args)
        {
            throw new NotImplementedException();
        }

        public void Subscribe(IPublisher publiser)
        {
            publiser.Add(this);
        }

        public void Unsubscribe(IPublisher publiser)
        {
            publiser.Remove(this);
        }
    }
}

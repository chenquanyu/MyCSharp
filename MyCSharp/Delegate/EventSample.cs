using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharp.Delegate
{
    public class CustomEventArgs : EventArgs
    {
        public DateTime PublishTime { get; set; }
    }

    public class Publisher
    {
        public event EventHandler<CustomEventArgs> Handler;

        public void RaiseEvent(CustomEventArgs args)
        {
            Handler?.Invoke(this, args);
        }
    }

    public class Subscriber
    {
        public void Subscribe(Publisher publiser)
        {
            publiser.Handler += HandleMethod;
        }

        public void Unsubscribe(Publisher publiser)
        {
            publiser.Handler -= HandleMethod;
        }

        public void HandleMethod(object publisher, CustomEventArgs args)
        {
            throw new NotImplementedException();
        }

    }

}

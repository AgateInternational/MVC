using System;
using System.Collections.Generic;

namespace Agate.MVC.Core
{
    public class Subscriber<TMessage> : ISubscribe
    {
        protected List<Action<TMessage>> _subscribers = new List<Action<TMessage>>();

        public void Add(Action<TMessage> subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Remove(Action<TMessage> subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        public void SendMessage(TMessage message)
        {
            foreach (var sub in _subscribers)
            {
                sub(message);
            }
        }
    }
}

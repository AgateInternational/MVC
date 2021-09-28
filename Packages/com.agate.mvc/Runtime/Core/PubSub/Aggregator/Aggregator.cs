using System;
using System.Collections.Generic;

namespace Agate.MVC.Core
{
    public class Aggregator
    {
        protected Dictionary<Type, ISubscribe> _subscribers = new Dictionary<Type, ISubscribe>();

        #region Type Identifier
        protected virtual Type GetMessageIdentifier<TMessage>()
        {
            return typeof(TMessage);
        }
        #endregion

        #region Publish

        public virtual void Publish<TMessage>(TMessage message)
        {
            var messageType = GetMessageIdentifier<TMessage>();

            if (_subscribers.ContainsKey(messageType))
            {
                var sub = _subscribers[messageType] as Subscriber<TMessage>;
                sub.SendMessage(message);
            }
        }
        #endregion

        #region Subsribe
        public virtual void Subscribe<TMessage>(Action<TMessage> subscriber)
        {
            var messageType = GetMessageIdentifier<TMessage>();

            if (!_subscribers.ContainsKey(messageType))
            {
                _subscribers.Add(messageType, new Subscriber<TMessage>());
            }

            var sub = _subscribers[messageType] as Subscriber<TMessage>;
            sub.Add(subscriber);
        }
        #endregion

        #region Unsubscribe
        public virtual void UnsubscribeAll()
        {
            _subscribers.Clear();
        }

        public virtual void UnsubscribeAll<TMessage>()
        {
            var messageType = GetMessageIdentifier<TMessage>();

            if (_subscribers.ContainsKey(messageType))
            {
                _subscribers.Remove(messageType);
            }
        }

        public virtual void Unsubscribe<TMessage>(Action<TMessage> subscriber)
        {
            var messageType = GetMessageIdentifier<TMessage>();

            if (_subscribers.ContainsKey(messageType))
            {
                var sub = _subscribers[messageType] as Subscriber<TMessage>;
                sub.Remove(subscriber);
            }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Starbugs.SimpleMessenger
{
    public class Dispatcher : IDispatcher
    {
        private static Dispatcher _instance;

        public static Dispatcher Default => _instance ??= new Dispatcher();

        private readonly Dictionary<Type, ISubscription> _subscriptions = new Dictionary<Type, ISubscription>();

        public int CallbacksCount => _subscriptions.Sum(x => (x.Value).CallbacksCount);

        public void Publish<T>(T message)
        {
            var messageType = message.GetType();

            if (_subscriptions.ContainsKey(messageType))
            {
                var subscription = (Subscription<T>) _subscriptions[messageType];

                foreach (var callback in subscription.Callbacks)
                {
                    if (callback.Precondition == null || callback.Precondition(message))
                    {
                        callback.Action(message);
                    }
                }
            }
        }

        public void Subscribe<T>(Action<T> action, Func<T, bool> precondition = null)
        {
            var type = typeof(T);

            Subscription<T> subscription;

            if (_subscriptions.ContainsKey(type))
            {
                subscription = (Subscription<T>) _subscriptions[type];
            }
            else
            {
                subscription = new Subscription<T>();
                _subscriptions.Add(type, subscription);
            }

            subscription.Add(action, precondition);
        }

        public void Unsubscribe<T>(Action<T> action)
        {
            var type = typeof(T);

            if (_subscriptions.ContainsKey(type))
            {
                var subscription = (Subscription<T>) _subscriptions[type];
                var callback = subscription.Callbacks.FirstOrDefault(c => c.Action == action);
                if (callback != null)
                {
                    subscription.Callbacks.Remove(callback);
                    if (subscription.Callbacks.Count == 0)
                    {
                        _subscriptions.Remove(type);
                    }
                }
            }
        }

        public void UnsubscribeAll()
        {
            _subscriptions.Clear();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace Nenuacho.SimpleMessenger.Scripts
{
    public class Dispatcher : IDispatcher
    {
        private static Dispatcher _instance;

        public static Dispatcher Default => _instance ??= new Dispatcher();

        private readonly Dictionary<Type, ISubscription> _subscriptions = new();

        public int CallbacksCount => _subscriptions.Sum(x => x.Value.CallbacksCount);

        public void Pub<T>(T message)
        {
            var type = typeof(T);

            if (_subscriptions.TryGetValue(type, out var result))
            {
                var subscription = (Subscription<T>) result;
                foreach (var callback in subscription.Callbacks)
                {
                    if (callback.Precondition == null || callback.Precondition(message))
                    {
                        callback.Action(message);
                    }
                }
            }
        }

        public void Sub<T>(Action<T> action, Func<T, bool> precondition = null)
        {
            var type = typeof(T);

            Subscription<T> subscription;

            if (_subscriptions.TryGetValue(type, out var result))
            {
                subscription = (Subscription<T>) result;
            }
            else
            {
                subscription = new Subscription<T>();
                _subscriptions.Add(type, subscription);
            }

            subscription.Add(action, precondition);
        }

        public void Unsub<T>(Action<T> action)
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

        public void UnsubAll()
        {
            _subscriptions.Clear();
        }
    }
}
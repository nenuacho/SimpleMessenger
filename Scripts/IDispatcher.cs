using System;

namespace Nenuacho.SimpleMessenger.Scripts
{
    public interface IDispatcher
    {
        void Pub<T>(T message);
        void Sub<T>(Action<T> action, Func<T, bool> precondition = null);
        void Unsub<T>(Action<T> action);
        void UnsubAll();
        public int CallbacksCount { get; }
    }
}
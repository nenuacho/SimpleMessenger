using System;

namespace Starbugs.SimpleMessenger
{
    public interface IDispatcher
    {
        void Publish<T>(T message);
        void Subscribe<T>(Action<T> action, Func<T, bool> precondition = null);
        void Unsubscribe<T>(Action<T> action);
        void UnsubscribeAll();
    }
}
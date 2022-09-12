using System;
using System.Collections.Generic;

namespace Starbugs.SimpleMessenger
{
    public class Subscription<T>
    {
        public List<CallbackWithPrecondition<T>> Callbacks { get; } = new();

        public void Add(Action<T> callback, Func<T, bool> precondition)
        {
            Callbacks.Add(new CallbackWithPrecondition<T>(callback, precondition));
        }
    }
}
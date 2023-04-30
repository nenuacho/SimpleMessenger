using System;
using System.Collections.Generic;
using Nenuacho.SimpleMessenger.Scripts;

namespace Starbugs.SimpleMessenger
{
    internal class Subscription<T> : ISubscription
    {
        public List<CallbackWithPrecondition<T>> Callbacks { get; } = new List<CallbackWithPrecondition<T>>();

        public void Add(Action<T> callback, Func<T, bool> precondition)
        {
            Callbacks.Add(new CallbackWithPrecondition<T>(callback, precondition));
        }

        public int CallbacksCount => Callbacks.Count;
    }

    internal interface ISubscription
    {
        int CallbacksCount { get; }
    }
}
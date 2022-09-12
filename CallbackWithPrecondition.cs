using System;

namespace Starbugs.SimpleMessenger
{
    public class CallbackWithPrecondition<T>
    {
        public Action<T> Action { get; }
        public Func<T, bool> Precondition { get; }

        public CallbackWithPrecondition(Action<T> action, Func<T, bool> precondition)
        {
            Action = action;
            Precondition = precondition;
        }
    }
}
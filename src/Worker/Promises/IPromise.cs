using System;

namespace Worker.Promises
{
    public interface IPromise<out T>
    {
        IPromise<T> Then(Action<T> onResolved, Action<Exception> onRejected = null);
        IPromise<U> Then<U>(Func<T, U> onResolved, Action<Exception> onRejected = null);
    }
}
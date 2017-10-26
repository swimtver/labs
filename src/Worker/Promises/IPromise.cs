using System;

namespace Worker.Promises
{
    public interface IPromise
    {
        void Then(Action action);
    }

    public interface IPromise<out T>
    {
    }
}
using System;
using System.Collections.Generic;

namespace Worker.Promises
{
    public class Promise<T> : IPromise<T>
    {
        private Queue<Action<T>> _handlers;
        private Action<Exception> _onRejected;
        private PromiseStates _state;
        private Exception _exception;
        private T _value;
        
        public IPromise<T> Then(Action<T> onResolved, Action<Exception> onRejected = null)
        {
            if (_state == PromiseStates.Pending)
            {
                AddHandler(onResolved);
                _onRejected = onRejected;
                return this;
            }
            if (_state == PromiseStates.Resolved)
                onResolved?.Invoke(_value);
            else
                onRejected?.Invoke(_exception);
            return this;
        }

        public IPromise<U> Then<U>(Func<T, U> onResolved, Action<Exception> onRejected = null)
        {
            var promise = new Promise<U>();
            if (_state == PromiseStates.Pending)
            {
                AddHandler(value =>
                {
                    var transformValue = onResolved.Invoke(value);
                    promise.Resolve(transformValue);
                });
                _onRejected = onRejected;
                return promise;
            }
            if (_state == PromiseStates.Resolved)
                promise.Resolve(onResolved.Invoke(_value));
            else
            {
                onRejected?.Invoke(_exception);
                promise.Reject(_exception);
            }
            return promise;
        }

        public void Resolve(T value)
        {
            if (_state != PromiseStates.Pending)
                throw new Exception("Can't resolve not pending promise");
            _value = value;
            _state = PromiseStates.Resolved;
            InvokeHandlers(_handlers);
        }

        public void Reject(Exception exception)
        {
            if (_state != PromiseStates.Pending)
                throw new Exception("Can't resolve not pending promise");

            _state = PromiseStates.Rejected;
            _exception = exception;

            _onRejected?.Invoke(_exception);
        }

        private void InvokeHandlers(Queue<Action<T>> queue)
        {
            if (queue == null) return;
            while (queue.Count > 0)
            {
                queue.Dequeue()?.Invoke(_value);
            }
        }

        private void AddHandler(Action<T> onResolved)
        {
            if (_handlers == null)
            {
                _handlers = new Queue<Action<T>>();
            }
            _handlers.Enqueue(onResolved);
        }
    }

    internal enum PromiseStates
    {
        Pending,
        Resolved,
        Rejected
    }
}
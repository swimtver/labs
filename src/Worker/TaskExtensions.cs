using System.Threading.Tasks;
using Worker.Promises;

namespace Worker
{
    internal static class TaskExtensions
    {
        internal static IPromise<T> ToPromise<T>(this Task<T> task)
        {
            var promise = new Promise<T>();
            if (task.IsCompleted)
            {
                ToResolvedPromise(task, promise);
            }
            else
            {
                task.ContinueWith(t => ToResolvedPromise(task, promise));
            }
            return promise;
        }

        private static void ToResolvedPromise<T>(Task<T> task, Promise<T> promise)
        {
            switch (task.Status)
            {
                case TaskStatus.RanToCompletion:
                    promise.Resolve(task.Result);
                    break;
                case TaskStatus.Faulted:
                    promise.Reject(task.Exception.InnerException);
                    break;
                case TaskStatus.Canceled:
                    promise.Reject(new TaskCanceledException(task));
                    break;
            }
        }
    }
}
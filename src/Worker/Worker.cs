using System;
using Worker.Execution;
using Worker.Promises;

namespace Worker
{
    public class Worker : IWorker
    {
        private readonly IRequestExecutor _executor;

        public Worker(IRequestExecutor executor)
        {
            _executor = executor;
        }

        public IPromise<Response> MakeRemoteRequest(Request request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            try
            {
                var task = _executor.Send(request);
                var promise = task.ToPromise();
                return promise;
            }
            catch(Exception ex)
            {
                var rejectedPromise = new Promise<Response>();
                rejectedPromise.Reject(ex);
                return rejectedPromise;
            }
        }
    }
}
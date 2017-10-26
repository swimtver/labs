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

        public IPromise MakeRemoteRequest(Request request)
        {
            if(request == null) throw new ArgumentNullException(nameof(request));
            try
            {
                _executor.Send(request);
            }
            catch
            {
            }
            finally
            {
                
            }
            return new Promise();
        }
    }
}
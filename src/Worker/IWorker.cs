using Worker.Execution;
using Worker.Promises;

namespace Worker
{
    public interface IWorker
    {
        IPromise<Response> MakeRemoteRequest(Request request);
    }
}
using System.Threading.Tasks;

namespace Worker.Execution
{
    public interface IRequestExecutor
    {
        Task<Response> Send(Request request);
    }
}
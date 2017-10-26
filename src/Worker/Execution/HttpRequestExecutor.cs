using System.Net.Http;
using System.Threading.Tasks;

namespace Worker.Execution
{
    public class HttpRequestExecutor : IRequestExecutor
    {
        private readonly HttpClient _httpClient;

        public HttpRequestExecutor(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Response> Send(Request request)
        {
            var responseMessage = await _httpClient.SendAsync(Create(request));
            return new Response();
        }

        private HttpRequestMessage Create(Request request)
        {
            return null;
        }
    }
}
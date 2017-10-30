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
            var responseMessage = await _httpClient.SendAsync(Create(request)).ConfigureAwait(false);
            return new Response((int)responseMessage.StatusCode);
        }

        private HttpRequestMessage Create(Request request)
        {
            return new HttpRequestMessage(new HttpMethod(request.Method), request.Uri);
        }
    }
}
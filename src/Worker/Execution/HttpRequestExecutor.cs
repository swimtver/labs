using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Worker.Execution
{
    public class HttpRequestExecutor : IRequestExecutor
    {
        private readonly HttpClient _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(3)
        };

        public async Task<Response> Send(Request request)
        {
            var responseMessage = await _httpClient.SendAsync(Create(request), HttpCompletionOption.ResponseContentRead).ConfigureAwait(false);
        
            var statusCode = (int)responseMessage.StatusCode;
            var contentType = responseMessage.Content.Headers.ContentType.MediaType;
            var content = await responseMessage.Content.ReadAsStringAsync();

            return new Response(statusCode)
            {
                ContentType = contentType,
                Content = content
            };
        }

        private HttpRequestMessage Create(Request request)
        {
            return new HttpRequestMessage(new HttpMethod(request.Method), request.Uri);
        }
    }
}
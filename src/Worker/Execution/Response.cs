namespace Worker.Execution
{
    public class Response
    {
        public Response(int statusCode)
        {
            StatusCode = statusCode;
        }

        public int StatusCode { get; }
    }
}
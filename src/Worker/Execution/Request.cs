using System;

namespace Worker.Execution
{
    public class Request
    {
        public Uri Uri { get; set; }
        public string Method { get; set; }

        public Request()
        {
        }
        public Request(Uri uri, string method)
        {
            Uri = uri;
            Method = method;
        }

    }
}
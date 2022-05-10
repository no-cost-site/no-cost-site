using System.Collections.Generic;

namespace NoCostSite.Api.Function
{
    public class HttpContext
    {
        private readonly Request _request;

        public HttpContext(Request request)
        {
            _request = request;
        }

        public IReadOnlyDictionary<string, string> Headers => _request.headers;
    }
}
using System.Collections.Generic;

namespace NoCostSite.ApiTests.Client
{
    internal class Request
    {
        public Request(string controller, string action, string body, Dictionary<string, string> headers)
        {
            Controller = controller;
            Action = action;
            Body = body;
            Headers = headers;
        }

        public string Controller { get; }
        
        public string Action { get; }
        
        internal string Body { get; }

        internal Dictionary<string, string> Headers { get; }
    }
}
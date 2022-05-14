using System.Collections.Generic;
using NoCostSite.ApiTests.Utils;

namespace NoCostSite.ApiTests.Client
{
    public class RequestBuilder
    {
        private string? _controller;
        private string? _action;
        private object _body = new {};
        private readonly Dictionary<string, string> _headers = new Dictionary<string, string>();

        public RequestBuilder WithController(string controller)
        {
            _controller = controller;
            return this;
        }

        public RequestBuilder WithAction(string action)
        {
            _action = action;
            return this;
        }

        public RequestBuilder WithBody(object body)
        {
            _body = body;
            return this;
        }

        public RequestBuilder WithHeader(string key, string value)
        {
            _headers[key] = value;
            return this;
        }

        public RequestBuilder WithToken(string token)
        {
            return WithHeader("Token", token);
        }

        internal Request Build()
        {
            Assert.Validate(() => !string.IsNullOrEmpty(_controller), "Controller should be configured");
            Assert.Validate(() => !string.IsNullOrEmpty(_action), "Action should be configured");
            
            return new Request(_controller!, _action!, _body.ToJson(), _headers);
        }
    }
}
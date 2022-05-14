using System;
using System.Net;

namespace NoCostSite.ApiTests.Client
{
    public class ApiException : Exception
    {
        public ApiException(string message, HttpStatusCode code)
            : base(message)
        {
            Code = code;
        }

        public HttpStatusCode Code { get; }
    }
}
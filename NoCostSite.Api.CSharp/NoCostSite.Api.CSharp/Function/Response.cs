using System;
using System.Collections.Generic;
using NoCostSite.Utils;

namespace NoCostSite.Api.CSharp.Function
{
    public class Response
    {
        public Response(int statusCode, string body)
        {
            StatusCode = statusCode;
            Body = body;
        }

        public int StatusCode { get; }

        public string Body { get; }

        public Dictionary<string, string> Headers { get; } = new Dictionary<string, string>
        {
            {"Access-Control-Allow-Headers", "*"},
            {"Access-Control-Allow-Methods", "*"},
            {"Access-Control-Allow-Origin", "*"},
        };

        public static Response Ok(object @object)
        {
            return Create(200, @object);
        }

        public static Response Validation(string message)
        {
            return Create(400, ResultResponse.Fail(message));
        }

        public static Response Unauthorized()
        {
            return Create(401, new NoContent());
        }

        public static Response NotFound()
        {
            return Create(404, new NoContent());
        }

        public static Response Error(Exception error)
        {
            return Create(500, new Error(error));
        }

        private static Response Create(int statusCode, object @object)
        {
            return new Response(statusCode, @object.ToJson());
        }
    }
}
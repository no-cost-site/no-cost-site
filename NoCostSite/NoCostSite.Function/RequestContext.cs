using System;
using System.Collections.Generic;
using System.Text;
using NoCostSite.Utils;

namespace NoCostSite.Function
{
    public class RequestContext
    {
        public object GetBody(Type type) => Body.ToObject(type);

        public string SecretAccessKey { get; set; } = null!;

        public string AccessKeyId { get; set; } = null!;

        public string? Token { get; set; }

        public string Action { get; set; } = null!;

        public string Controller { get; set; } = null!;

        private string Body { get; set; } = null!;

        public static RequestContext Create(Request request)
        {
            return new RequestContext
            {
                Body = ExtractBody(request),
                Controller = request.queryStringParameters["Controller"],
                Action = request.queryStringParameters["Action"],
                Token = request.headers.GetValueOrDefault("Token"),
                AccessKeyId = Environment.GetEnvironmentVariable("AccessKeyId")!,
                SecretAccessKey = Environment.GetEnvironmentVariable("SecretAccessKey")!,
            };
        }

        private static string ExtractBody(Request request)
        {
            return request.isBase64Encoded
                ? Encoding.UTF8.GetString(Convert.FromBase64String(request.body))
                : request.body;
        }
    }
}
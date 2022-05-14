using System;
using System.Collections.Generic;
using System.Text;
using NoCostSite.Utils;

namespace NoCostSite.Function
{
    public class RequestContext
    {
        public object GetBody(Type type) => Body.ToObject(type);

        public string ObjectStorageSecretAccessKey { get; private set; } = null!;

        public string ObjectStorageAccessKeyId { get; private set; } = null!;

        public string ObjectStorageServiceUrl { get; private set; } = null!;

        public string ObjectStorageRegion { get; private set; } = null!;

        public string TokenIssuer { get; private set; } = null!;

        public string TokenAudience { get; private set; } = null!;

        public string TokenSecureKey { get; private set; } = null!;

        public int TokenExpirationDays { get; private set; }

        public string DataBaseSecureKey { get; private set; } = null!;

        public string? Token { get; private set; }

        public string Action { get; private set; } = null!;

        public string Controller { get; private set; } = null!;

        private string Body { get; set; } = null!;
        
        public string PublicBucketName { get; private set; } = null!;
        
        public string PrivateBucketName { get; private set; } = null!;

        public static RequestContext Create(Request request)
        {
            return new RequestContext
            {
                Body = ExtractBody(request),
                Controller = request.queryStringParameters["Controller"],
                Action = request.queryStringParameters["Action"],
                Token = request.headers.GetValueOrDefault("Token"),
                PublicBucketName = Environment.GetEnvironmentVariable("PublicBucketName")!,
                PrivateBucketName = Environment.GetEnvironmentVariable("PrivateBucketName")!,
                ObjectStorageAccessKeyId = Environment.GetEnvironmentVariable("ObjectStorageAccessKeyId")!,
                ObjectStorageSecretAccessKey = Environment.GetEnvironmentVariable("ObjectStorageSecretAccessKey")!,
                ObjectStorageServiceUrl = Environment.GetEnvironmentVariable("ObjectStorageServiceUrl")!,
                ObjectStorageRegion = Environment.GetEnvironmentVariable("ObjectStorageRegion")!,
                TokenIssuer = Environment.GetEnvironmentVariable("TokenIssuer")!,
                TokenAudience = Environment.GetEnvironmentVariable("TokenAudience")!,
                TokenSecureKey = Environment.GetEnvironmentVariable("TokenSecureKey")!,
                TokenExpirationDays = int.Parse(Environment.GetEnvironmentVariable("TokenExpirationDays")!),
                DataBaseSecureKey = Environment.GetEnvironmentVariable("DataBaseSecureKey")!,
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
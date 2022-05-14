using System;
using NoCostSite.BusinessLogic.Config;
using NoCostSite.Function;

namespace NoCostSite.Api.Filters
{
    public class SetupSettingsFilter : IRequestFilter
    {
        public void Filter(RequestContext context)
        {
            ConfigContainer.Current = new Config
            {
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
    }
}
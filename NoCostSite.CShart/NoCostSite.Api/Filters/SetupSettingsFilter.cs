using NoCostSite.BusinessLogic.Settings;
using NoCostSite.Function;

namespace NoCostSite.Api.Filters
{
    public class SetupSettingsFilter : IRequestFilter
    {
        public void Filter(RequestContext context)
        {
            SettingsContainer.Current = new Settings
            {
                PublicBucketName = context.PublicBucketName,
                PrivateBucketName = context.PrivateBucketName,
                ObjectStorageSecretAccessKey = context.ObjectStorageSecretAccessKey,
                ObjectStorageAccessKeyId = context.ObjectStorageAccessKeyId,
                ObjectStorageServiceUrl = context.ObjectStorageServiceUrl,
                ObjectStorageRegion = context.ObjectStorageRegion,
                TokenIssuer = context.TokenIssuer,
                TokenAudience = context.TokenAudience,
                TokenSecureKey = context.TokenSecureKey,
                TokenExpirationDays = context.TokenExpirationDays,
                DataBaseSecureKey = context.DataBaseSecureKey
            };
        }
    }
}
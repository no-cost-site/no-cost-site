using NoCostSite.BusinessLogic.Settings;

namespace NoCostSite.BusinessLogic.ObjectStorage
{
    public class ObjectStorageClientFactory
    {
        public ObjectStorageClient Create(string bucketName)
        {
            var config = SettingsContainer.Current;
            return new ObjectStorageClient(new ObjectStorageClientConfig
            {
                AccessKeyId = config.ObjectStorageAccessKeyId,
                SecretAccessKey = config.ObjectStorageSecretAccessKey,
                ServiceUrl = config.ObjectStorageServiceUrl,
                AuthenticationRegion = config.ObjectStorageRegion,
                BucketName = bucketName,
            });
        }
    }
}
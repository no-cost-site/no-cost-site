namespace NoCostSite.ObjectStorage.Client
{
    public class ObjectStorageClientFactory
    {
        public ObjectStorageClient Create(string bucketName)
        {
            var config = ObjectStorageClientAccessConfigContainer.Current;
            return new ObjectStorageClient(new ObjectStorageClientConfig
            {
                AccessKeyId = config.AccessKeyId,
                SecretAccessKey = config.SecretAccessKey,
                ServiceUrl = config.ServiceUrl,
                AuthenticationRegion = config.AuthenticationRegion,
                BucketName = bucketName,
            });
        }
    }
}
namespace NoCostSite.ObjectStorage.Client
{
    public class ObjectStorageClientConfig
    {
        public string AccessKeyId { get; set; } = null!;

        public string SecretAccessKey { get; set; } = null!;

        public string ServiceUrl { get; set; } = null!;

        public string AuthenticationRegion { get; set; } = null!;

        public string BucketName { get; set; } = null!;
    }
}
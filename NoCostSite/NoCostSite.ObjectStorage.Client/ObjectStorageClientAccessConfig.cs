namespace NoCostSite.ObjectStorage.Client
{
    public class ObjectStorageClientAccessConfig
    {
        public string AccessKeyId { get; set; } = null!;

        public string SecretAccessKey { get; set; } = null!;

        public string ServiceUrl { get; set; } = null!;

        public string AuthenticationRegion { get; set; } = null!;
    }
}
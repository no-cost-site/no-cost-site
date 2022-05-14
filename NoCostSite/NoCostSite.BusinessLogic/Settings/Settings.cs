namespace NoCostSite.BusinessLogic.Settings
{
    public class Settings
    {
        public string PublicBucketName { get; set; } = null!;

        public string PrivateBucketName { get; set; } = null!;

        public string ObjectStorageSecretAccessKey { get; set; } = null!;

        public string ObjectStorageAccessKeyId { get; set; } = null!;

        public string ObjectStorageServiceUrl { get; set; } = null!;

        public string ObjectStorageRegion { get; set; } = null!;
        
        public string TokenIssuer { get; set; } = null!;

        public string TokenAudience { get; set; } = null!;

        public string TokenSecureKey { get; set; } = null!;

        public int TokenExpirationDays { get; set; }

        public string DataBaseSecureKey { get; set; } = null!;
    }
}
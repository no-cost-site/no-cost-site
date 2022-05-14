namespace NoCostSite.BusinessLogic.Settings
{
    public class Settings
    {
        public string PublicBucketName { get; set; } = null!;

        public string PrivateBucketName { get; set; } = null!;
        
        public string TokenIssuer { get; set; } = null!;

        public string TokenAudience { get; set; } = null!;

        public string TokenSecureKey { get; set; } = null!;

        public string DataBaseSecureKey { get; set; } = null!;

        public int TokenExpirationDays { get; set; }
    }
}
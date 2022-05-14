namespace NoCostSite.BusinessLogic.Settings
{
    public class Settings
    {
        public string PublicBucketName { get; set; } = null!;

        public string PrivateBucketName { get; set; } = null!;
        
        public string TokenIssuer { get; set; } = null!;

        public string TokenAudience { get; set; } = null!;

        public string SecureKey { get; set; } = null!;

        public int TokenExpirationDays { get; set; }
    }
}
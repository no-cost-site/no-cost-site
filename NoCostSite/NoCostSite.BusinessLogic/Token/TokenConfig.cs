namespace NoCostSite.BusinessLogic.Token
{
    public class TokenConfig
    {
        public string Issuer { get; } = null!;

        public string Audience { get; set; } = null!;

        public string Key { get; } = null!;
        
        public int ExpirationDays { get; set; }
    }
}
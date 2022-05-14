namespace NoCostSite.Api.Dto.Auth
{
    public class AuthIsInitResponse
    {
        public bool IsInit { get; set; }

        public static AuthIsInitResponse Ok(bool isInit)
        {
            return new AuthIsInitResponse
            {
                IsInit = isInit,
            };
        }
    }
}
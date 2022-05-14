namespace NoCostSite.Api.Dto.Auth
{
    public class AuthLoginResponse
    {
        public bool IsLogin { get; set; }

        public string? Token { get; set; }

        public static AuthLoginResponse Ok(string token)
        {
            return new AuthLoginResponse
            {
                IsLogin = true,
                Token = token,
            };
        }
    }
}
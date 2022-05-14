namespace NoCostSite.Api.Dto.Auth
{
    public class AuthRegisterRequest
    {
        public string? Password { get; set; }
        
        public string? PasswordConfirm { get; set; }
    }
}
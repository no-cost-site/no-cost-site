namespace NoCostSite.Api.Dto
{
    public class AuthRegisterRequest
    {
        public string? Password { get; set; }
        
        public string? PasswordConfirm { get; set; }
    }
}
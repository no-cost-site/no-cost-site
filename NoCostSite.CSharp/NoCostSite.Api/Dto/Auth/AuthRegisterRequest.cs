using NoCostSite.Api.Dto.Settings;

namespace NoCostSite.Api.Dto.Auth
{
    public class AuthRegisterRequest
    {
        public SettingsDto? Settings { get; set; }
        
        public string? Password { get; set; }
        
        public string? PasswordConfirm { get; set; }
    }
}
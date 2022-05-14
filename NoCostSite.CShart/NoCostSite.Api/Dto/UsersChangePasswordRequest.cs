namespace NoCostSite.Api.Dto
{
    public class UsersChangePasswordRequest
    {
        public string? OldPassword { get; set; }
        
        public string? NewPassword { get; set; }
        
        public string? PasswordConfirm { get; set; }
    }
}
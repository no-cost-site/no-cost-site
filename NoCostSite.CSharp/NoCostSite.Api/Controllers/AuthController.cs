using System.Threading.Tasks;
using NoCostSite.Api.Dto.Auth;
using NoCostSite.BusinessLogic.Auth;
using NoCostSite.BusinessLogic.Settings;
using NoCostSite.BusinessLogic.Users;
using NoCostSite.Function;

namespace NoCostSite.Api.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService = new AuthService();
        private readonly UsersService _usersService = new UsersService();
        private readonly SettingsService _settingsService = new SettingsService();
        
        public Task<ResultResponse> Check()
        {
            var isAuth = _authService.IsAuth(Context.Token!);
            var result = isAuth ? ResultResponse.Ok() : ResultResponse.Fail("Token invalid");
            return Task.FromResult(result);
        }

        public async Task<AuthLoginResponse> Login(AuthLoginRequest request)
        {
            var token = await _authService.Login(request.Password);
            return AuthLoginResponse.Ok(token);
        }

        public async Task<ResultResponse> Register(AuthRegisterRequest request)
        {
            var settings = new Settings
            {
                Language = request.Settings!.Language,
            };

            await _usersService.Create(request.Password, request.PasswordConfirm);
            await _settingsService.Upsert(settings);
            return ResultResponse.Ok();
        }

        public async Task<AuthIsInitResponse> IsInit()
        {
            var userExists = await _usersService.Exists();
            return AuthIsInitResponse.Ok(userExists);
        }
    }
}
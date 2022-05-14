using System.Threading.Tasks;
using NoCostSite.Api.Dto;
using NoCostSite.BusinessLogic.Auth;
using NoCostSite.BusinessLogic.Users;
using NoCostSite.Function;

namespace NoCostSite.Api.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService = new AuthService();
        private readonly UsersService _usersService = new UsersService();
        
        public async Task<object> Login(AuthLoginRequest request)
        {
            var token = await _authService.Login(request.Password);
            return AuthLoginResponse.Ok(token);
        }

        public async Task<object> Register(AuthRegisterRequest request)
        {
            await _usersService.Create(request.Password, request.PasswordConfirm);
            return ResultResponse.Ok();
        }
    }
}
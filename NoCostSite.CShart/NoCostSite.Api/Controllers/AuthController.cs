using System.Threading.Tasks;
using NoCostSite.Api.Dto;
using NoCostSite.BusinessLogic.Auth;
using NoCostSite.Function;

namespace NoCostSite.Api.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService = new AuthService();
        
        public async Task<object> Login(AuthLoginRequest request)
        {
            var token = await _authService.Login(request.Password);
            return AuthLoginResponse.Ok(token);
        }
    }
}